namespace CompanyManagement.API.Helpers
{
    public static class StringHelper
    {
        public static bool IsNullOrEmpty(this string s)
        {
            return string.IsNullOrEmpty(s);
        }

        public static bool IsNotNullOrEmpty(this string s)
        {
            return !IsNullOrEmpty(s);
        }

        public static bool IsNullOrWhiteSpace(this string s)
        {
            return string.IsNullOrWhiteSpace(s);
        }

        public static bool IsNotNullOrWhiteSpace(this string s)
        {
            return !IsNullOrWhiteSpace(s);
        }

        public static string Truncate(this string value, int maxLength)
        {
            return value.Length > maxLength ? value[..maxLength] : value;
        }
    }
}