using System.Text.Json.Serialization;

using CompanyManagement.API.Enums;

using Newtonsoft.Json;

namespace CompanyManagement.API.ViewModels
{
    public class CompaniesListResultViewModel
    {
        public CompanyPaginationViewModel Pagination { get; set; }=new CompanyPaginationViewModel();
        public List<CompanyViewModel> Companies { get; set; } = new List<CompanyViewModel>();
    }

}
