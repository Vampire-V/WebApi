using NLog.Web;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using WebApi.Config;
using WebApi.Data.ChequeDirect;
using WebApi.Data.UserContext;
using WebApi.Data.Monitoring;
using WebApi.Data.S4;
using WebApi.Data.NitgenAccessManager;
using WebApi.Data.Accounting;
using WebApi.Data.ChequeBNP;
using Swashbuckle.AspNetCore.SwaggerUI;
using WebApi.Hubs;
using Hangfire;
using HangfireBasicAuthenticationFilter;
using WebApi.Extensions;
using WebApi.Data.Hangfire;
using WebApi.Middleware;
using Swashbuckle.AspNetCore.Filters;
using Microsoft.AspNetCore.Mvc;
using WebApi.Data.CosmoIm9773;
using WebApi.Data.CosmoWms9773;
using WatchDog;
using WebApi.Data.WatchDogs;

var builder = WebApplication.CreateBuilder(args);
// NLog: Setup NLog for Dependency injection
builder.Host.ConfigureLogging(logging =>
    {
        // logging.ClearProviders();
        logging.SetMinimumLevel(LogLevel.Trace);
    }).UseNLog();

// Config server option    

// builder.WebHost.ConfigureKestrel(serverOptions =>
// {
//     serverOptions.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(5);
//     serverOptions.Limits.MaxResponseBufferSize = null;
// });

// Add services to the container.
builder.Services.AddControllers(options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true)
                .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Description = "Bird kak Auth",
        Type = SecuritySchemeType.ApiKey
    });
    options.OperationFilter<SecurityRequirementsOperationFilter>();
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Bird kak API",
        Description = "Haier API",
    });
    // Set the comments path for the Swagger JSON and UI.
    // options.SwaggerGenerato

});

IConfigurationRoot configuration;
if (builder.Environment.EnvironmentName == "Development")
{
    configuration = builder.Configuration.SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.Development.json").Build();
}
else
{
    configuration = builder.Configuration.SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
}

// Config Authenticate JWT
var _authkey = configuration.GetValue<string>("JwtSettings:Key");
builder.Services.AddAuthentication(item =>
{
    item.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    item.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(item =>
{
    item.RequireHttpsMetadata = true;
    item.SaveToken = true;
    item.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authkey)),
        ValidateIssuer = false,
        ValidateAudience = false,
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddDbContext<CosmoIm9773>();
builder.Services.AddDbContext<CosmoWms9773>();
builder.Services.AddDbContext<ChequeDirect>();
builder.Services.AddDbContext<ChequeBNP>();
builder.Services.AddDbContext<UserContext>();
builder.Services.AddDbContext<NitgenAccessManager>();
builder.Services.AddDbContext<S4>();
builder.Services.AddDbContext<Accounting>();
builder.Services.AddDbContext<Monitoring>();
// Hangfire service background jobs SQL Server 2005 ที่ทำงานไม่รองรับ
builder.Services.RegisterHangfire(configuration);
builder.Services.RegisterWatchDog(configuration);
// Add DbContrxt, Service, Repo

builder.Services.AddScoped<ValidationFilterAttribute>();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.RegisterServices(configuration);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
// Configure
builder.Services.Configure<ApiBehaviorOptions>(o => o.SuppressModelStateInvalidFilter = true);
builder.Services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
builder.Services.Configure<MailConfig>(configuration.GetSection("Smtp"));
builder.Services.Configure<FtpSettings>(configuration.GetSection("FtpBackup"));
builder.Services.Configure<FolderBackUp>(configuration.GetSection("FolderBackUp"));
builder.Services.Configure<LineDev>(configuration.GetSection("LineDev"));
// Policy Cors
builder.Services.AddCors(options => options
    .AddPolicy("CorsPolicy", o => o
        .WithOrigins(configuration["Origins"].Split(';'))
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials())
);
builder.Services.AddSignalR();

var app = builder.Build();
// Configure the HTTP request pipeline.

// if (app.Environment.IsDevelopment() || !app.Environment.IsDevelopment())
// {
app.UseSwagger();
app.UseSwaggerUI(s =>
{
    s.SwaggerEndpoint("/swagger/v1/swagger.json", "Bird Kak API all version");
    s.RoutePrefix = string.Empty;
    s.DocumentTitle = "Bird Kak";
    s.DocExpansion(DocExpansion.None);
});

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders.XForwardedFor | Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders.XForwardedProto
});
app.UseWatchDogExceptionLogger();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCors("CorsPolicy");
app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();
// app.UseMiddleware<JwtMiddleware>();

app.UseWatchDog(conf =>
{
    conf.WatchPageUsername = configuration.GetValue<string>("WatchDogSettings:Username");
    conf.WatchPagePassword = configuration.GetValue<string>("WatchDogSettings:Password");
    conf.Blacklist = "hangfire/stats";
});

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    // Real-time ASP.NET with SignalR
    endpoints.MapHub<EmployeeImageHub>("/employeeImageHub");
    endpoints.MapHangfireDashboard("/hangfire", new DashboardOptions
    {
        Authorization = new[] {
                new HangfireCustomBasicAuthenticationFilter{
                    User = configuration.GetValue<string>("HangfireSettings:Username"),
                    Pass = configuration.GetValue<string>("HangfireSettings:Password")
                }
        }
    });
    endpoints.MapGet("/debug/routes", (IEnumerable<EndpointDataSource> endpointSources) => string.Join("\n", endpointSources.SelectMany(source => source.Endpoints)));
    // HangfireDashboard background jobs
    // endpoints.MapHangfireDashboard(); // BackgroundJob.Enqueue(() => Console.WriteLine("Hello from Hangfire!"));
});

app.Run();