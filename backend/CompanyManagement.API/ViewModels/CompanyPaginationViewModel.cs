using Newtonsoft.Json;

namespace CompanyManagement.API.ViewModels
{
    public class CompanyPaginationViewModel
    {
        [JsonProperty("totalRecords")]
        public int TotalRecords { get; set; }
        [JsonProperty("currentPageRecords")]
        public int CurrentPageRecords { get; set; }
        [JsonProperty("pageSize")]
        public int PageSize { get; set; } = 5;
        [JsonProperty("pageIndex")]
        public int PageIndex { get; set; } = 0;
        [JsonProperty("startIndex")]
        public int StartIndex { get; set; } = 0;

        [JsonProperty("endIndex")]
        public int EndIndex { get; set; }

        [JsonProperty("sortBy")]
        public string SortBy { get; set; } = "CompanyName";

        [JsonProperty("sortOrder")]
        public string SortOrder { get; set; } = "asc";
    }

}
