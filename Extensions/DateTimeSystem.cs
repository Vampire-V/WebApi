using System.Globalization;

namespace WebApi.Extensions
{
    public static class DateTimeSystem
    {
        private static string[] formatDate = { "yyyyMMdd", "yyyy-MM-dd" };
        private static IConfiguration? AppSetting = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
        public static TimeZoneInfo TimeZone = TimeZoneInfo.FindSystemTimeZoneById(AppSetting.GetValue<string>("TimeZone"));
        public static Func<DateTime, DateTime> Utc = datetime => TimeZoneInfo.ConvertTimeFromUtc(datetime, TimeZone);
        public static Func<string, DateTime> ToDateTime = datestr => DateTime.ParseExact(datestr.Trim(), formatDate, CultureInfo.InvariantCulture);
    }
}