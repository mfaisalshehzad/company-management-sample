using Newtonsoft.Json;

namespace CompanyManagement.API.ViewModels
{
    public class UpdateCompanyViewModel
    {
        [JsonProperty("id")]
        public Guid? Id { get; set; }

        [JsonProperty("companyName")]
        public required string CompanyName { get; set; }
        [JsonProperty("industry")]
        public required string Industry { get; set; }
        [JsonProperty("numberOfEmployees")]
        public int NumberOfEmployees { get; set; }
        [JsonProperty("city")]
        public required string City { get; set; }
        [JsonProperty("parentCompanyId")]
        public Guid? ParentCompanyId { get; set; }
    }
}
