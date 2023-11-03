using System.Net;

namespace CompanyManagement.API.Exceptions
{
    public class InvalidLoginException : ApiException
    {
        public InvalidLoginException(string message): base("LOGIN_FAILED", HttpStatusCode.Unauthorized, message) { }
    }
}
