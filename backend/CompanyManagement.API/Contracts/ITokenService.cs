using CompanyManagement.API.Models;
using CompanyManagement.API.ViewModels;

using System.Security.Claims;

namespace CompanyManagement.API.Contracts
{
    public interface ITokenService
    {
        Task<TokenDto> CreateAuthToken(AuthUser user);
    }
}
