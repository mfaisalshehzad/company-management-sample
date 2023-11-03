namespace CompanyManagement.API.Helpers
{
    public static class GuidHelper
    {
        public static bool IsEmpty(this Guid? guid)
        {
            return guid == Guid.Empty;
        }

        public static bool IsNotEmpty(this Guid? guid)
        {
            return !IsEmpty(guid);
        }

        public static bool IsNullOrEmpty(this Guid? guid)
        {
            return guid == null || guid == Guid.Empty;
        }

        public static bool IsNotNullOrEmpty(this Guid? guid)
        {
            return !IsNullOrEmpty(guid);
        }

        public static Guid? TryParse(this string id)
        {
            Guid guid;
            return Guid.TryParse(id, out guid) ? (Guid?)guid : null;
        }

        public static Guid Parse(this string id)
        {
            Guid guid = Guid.Empty;
            return Guid.TryParse(id, out guid) ? guid : Guid.Empty;
        }
    }
}
