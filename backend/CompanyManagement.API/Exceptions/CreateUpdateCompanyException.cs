namespace CompanyManagement.API.Exceptions
{
    public class CreateUpdateCompanyException : ApiException
    {
        public CreateUpdateCompanyException(string message) : base("CREATE_UPDATE_COMPANY_EXCEPTION", System.Net.HttpStatusCode.BadRequest, message) { }
    }
}
