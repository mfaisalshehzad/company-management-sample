namespace CompanyManagement.API.Helpers
{
    public static class PasswordHelper
    {
        public static int? GetPasswordValidityDays(IConfiguration config, Guid countryId, DateTime? expiresAt)
        {
            if (expiresAt == null) return null;

            var countryTimeZone = config.GetValue<string>($"country:{countryId}:countryTimeZone");
            if (string.IsNullOrEmpty(countryTimeZone)) throw new Exception($"Country time zone for '{countryId}' is missing.");

            var expiredAt = UnixTimeStampMsHelper.GetLocalStartDateInUTC(expiresAt.Value, countryTimeZone).DateTime;
            var today = UnixTimeStampMsHelper.GetLocalStartDateInUTC(DateTime.UtcNow, countryTimeZone).DateTime;

            return (int)expiredAt.Subtract(today).TotalDays;
        }
    }
}
