using System.Net;

namespace CompanyManagement.API.Exceptions
{
    public class OperationException : ApiException
    {
        public OperationException(string message) : base("OP_EXCEPTION", HttpStatusCode.BadRequest, message)
        {
        }
    }
}
