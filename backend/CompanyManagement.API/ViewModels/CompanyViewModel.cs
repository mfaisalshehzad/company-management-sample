using Newtonsoft.Json;

namespace CompanyManagement.API.ViewModels
{
    public class CompanyViewModel
    {
        [JsonProperty("id")]
        public Guid? Id { get; set; }

        [JsonProperty("companyNo")]
        public int CompanyNo { get; private set; }

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
        [JsonProperty("parentCompany")]
        public string ParentCompany { get; set; } = string.Empty;
        [JsonProperty("companyLevel")]
        public int CompanyLevel { get; set; }
        [JsonProperty("createdBy")]
        public Guid CreatedBy { get; set; }
        [JsonProperty("createdAt")]
        public DateTime CreatedAt { get; set; }
        [JsonProperty("updatedBy")]
        public Guid? UpdatedBy { get; set; }
        [JsonProperty("updatedAt")]
        public DateTime? UpdatedAt { get; set; }
    }
}
