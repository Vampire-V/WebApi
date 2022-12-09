using Microsoft.AspNetCore.Antiforgery;
namespace WebApi.Middleware
{
    public class AntiforgeryMiddleware : IMiddleware
{
    private readonly IAntiforgery _antiforgery;

    public AntiforgeryMiddleware(IAntiforgery antiforgery)
    {
        _antiforgery = antiforgery;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var isGetRequest = string.Equals("GET", context.Request.Method, StringComparison.OrdinalIgnoreCase);
        if (!isGetRequest)
        {
            _antiforgery.ValidateRequestAsync(context).GetAwaiter().GetResult();
        }

        await next(context);
    }
}
}