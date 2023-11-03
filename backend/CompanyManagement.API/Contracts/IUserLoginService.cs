using CompanyManagement.API.ViewModels;

using Microsoft.AspNetCore.Mvc;

namespace CompanyManagement.API.Contracts
{
    public interface IUserLoginService
    {
        Task<IActionResult> LoginUser(UserLoginViewModel model);
        Task<IActionResult> GetUser();
        Task<IActionResult> ChangePassword(ChangePasswordViewModel model);
    }
}
