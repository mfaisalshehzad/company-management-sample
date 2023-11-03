using CompanyManagement.API.Contracts;
using CompanyManagement.API.DBContexts;
using CompanyManagement.API.Exceptions;
using CompanyManagement.API.Helpers;
using CompanyManagement.API.Models;
using CompanyManagement.API.ViewModels;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Newtonsoft.Json.Linq;

namespace CompanyManagement.API.Services
{
    public class UserLoginService : IUserLoginService
    {
        private const string LOGIN_ERROR_MESSAGE = "User email or password is invalid.";
        private readonly ILogger<UserLoginService> _logger;
        private readonly AppDbContext _dbContext;
        private readonly IConfiguration _config;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly ITokenService _tokenService;

        public UserLoginService(AppDbContext dbContext,
            ITokenService tokenService,
            IConfiguration config,
            IHttpContextAccessor contextAccessor,
            ILogger<UserLoginService> logger)
        {
            _logger = logger;
            _config = config;
            _dbContext = dbContext;
            _contextAccessor = contextAccessor;
            _tokenService = tokenService;
        }

        public async Task<IActionResult> LoginUser(UserLoginViewModel model)
        {
            try
            {
                var token = await TryLoginUser(model.Email, model.Password);
                return token == null ? new BadRequestResult() : (ActionResult)new OkObjectResult(token);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, $"Error - UserLoginService_LoginUser(Email: {model.Email}): {ex}");
                throw new OperationException(ex.Message);
            }
        }

        public async Task<IActionResult> GetUser()
        {
            try
            {
                var userId = _contextAccessor.HttpContext?.GetUserId();
                var user = await _dbContext.AuthUsers.FirstOrDefaultAsync(u => u.Id == userId);

                if (user == null)
                    return new BadRequestResult();

                return new OkObjectResult(new AuthUserViewModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName
                });
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            try
            {
                var errorMessage = "Change password failed";
                var userId = _contextAccessor.HttpContext?.GetUserId();
                var authUser = await _dbContext.AuthUsers.FirstOrDefaultAsync(u => u.Id == userId);

                if (authUser == null)
                {
                    _logger.LogWarning($"Fail - UserLoginService_ChangePassword(userId: {userId}): User Not found.");
                    throw new ChangePasswordException(errorMessage);
                }

                var token = await TryLoginUser(authUser, model.CurrentPassword);

                if (token == null)
                {
                    _logger.LogWarning($"Fail - UserLoginService_ChangePassword(Email: {authUser.Email}): Failed to save changes.");
                    throw new ChangePasswordException(errorMessage);
                }

                var isInputValid = model.NewPassword.IsNotNullOrEmpty() && model.ConfirmPassword.IsNotNullOrEmpty() && model.NewPassword.Equals(model.ConfirmPassword);

                if (!isInputValid)
                {
                    throw new ChangePasswordException(errorMessage);
                }

                // change password
                authUser.Salt = BCrypt.Net.BCrypt.GenerateSalt();
                authUser.Password = BCrypt.Net.BCrypt.HashPassword(model.NewPassword, authUser.Salt);

                var trans = await _dbContext.SaveChangesAsync();

                if (trans == 0)
                {
                    _logger.LogWarning($"Fail - UserLoginService_ChangePassword(Email: {authUser.Email}): Failed to save changes.");
                    throw new ChangePasswordException(errorMessage);
                }

                return new OkResult();
            }
            catch (AppException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error - UserLoginService_ChangePassword: {ex}");
                throw new OperationException(ex.Message);
            }
        }

        #region Private Methods

        private async Task<TokenDto> TryLoginUser(string email, string password)
        {
            var authUser = await _dbContext.AuthUsers.FirstOrDefaultAsync(u => u.Email == email);

            if (authUser == null)
            {
                _logger.LogWarning($"Fail - UserLoginService_TryLoginUser(Email: {email}): auth user not found.");
                throw new InvalidLoginException(LOGIN_ERROR_MESSAGE);
            }

            return await TryLoginUser(authUser, password);
        }

        private async Task<TokenDto> TryLoginUser(AuthUser authUser, string password)
        {
            if (authUser.Password != BCrypt.Net.BCrypt.HashPassword(password, authUser.Salt))
            {
                _logger.LogWarning($"Fail - UserLoginService_TryLoginUser(Email: {authUser.Email}): Password doesn't match.");
                throw new InvalidLoginException(LOGIN_ERROR_MESSAGE);
            }

            var token = await _tokenService.CreateAuthToken(authUser);

            return token;
        }

        #endregion
    }
}
