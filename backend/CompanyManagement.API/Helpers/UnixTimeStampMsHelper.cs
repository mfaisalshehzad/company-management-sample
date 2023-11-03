using NodaTime;

namespace CompanyManagement.API.Helpers
{
    public static class UnixTimeStampMsHelper
    {
        public static long? DateTimeToLong(DateTime? date)
            => date.HasValue ? new DateTimeOffset(DateTime.SpecifyKind((DateTime)date, DateTimeKind.Utc)).ToUnixTimeMilliseconds() : (long?)null;

        public static DateTimeOffset GetLocalStartDateInUTC(DateTime date, string countryTimeZone)
        {
            var zone = DateTimeZoneProviders.Tzdb[countryTimeZone];
            var instant = Instant.FromDateTimeUtc(DateTime.SpecifyKind(date, DateTimeKind.Utc));
            var localDateTime = instant.InZone(zone).ToDateTimeOffset();
            var localDate = localDateTime.AddTicks(localDateTime.TimeOfDay.Ticks * -1);
            var local = LocalDateTime.FromDateTime(localDate.DateTime);
            var zoned = local.InZoneStrictly(zone);
            var utc = zoned.WithZone(DateTimeZone.Utc).ToDateTimeOffset();
            return utc;
        }
    }
}
