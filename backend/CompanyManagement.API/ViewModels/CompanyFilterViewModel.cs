using System.Text.Json.Serialization;

using CompanyManagement.API.Enums;

using Newtonsoft.Json;

namespace CompanyManagement.API.ViewModels
{
    public class CompanyFilterViewModel
    {
        [JsonPropertyName("companyNo")]
        public int? CompanyNo { get; set; }
        [JsonPropertyName("companyName")]
        public string? CompanyName { get; set; }
        [JsonPropertyName("industry")]
        public string? Industry { get; set; }
        [JsonPropertyName("city")]
        public string? City { get; set; }
        [JsonPropertyName("parentCompany")]
        public string? ParentCompany { get; set; }
        [JsonProperty("sortBy")]
        public string SortBy { get; set; } = "CompanyName";

        [JsonProperty("sortOrder")]
        public string SortOrder { get; set; } = "asc";

        [JsonProperty("pageIndex")]
        public int PageIndex { get; set; } = 0;

        [JsonProperty("pageSize")]
        public int PageSize { get; set; } = 5;
    }
    
}
