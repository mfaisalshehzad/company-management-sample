using CompanyManagement.API.ViewModels;

using Microsoft.AspNetCore.Mvc;

namespace CompanyManagement.API.Contracts
{
    public interface ICompanyService
    {
        Task<CompanyViewModel> Get(Guid id);
        /// <summary>
        /// Get all the companies list for Dropdown
        /// </summary>
        /// <returns></returns>
        Task<dynamic> GetAll();
        Task<CompaniesListResultViewModel> GetList(CompanyFilterViewModel model);
        Task<CompanyViewModel> CreateOrUpdateCompany(UpdateCompanyViewModel model);
    }
}
