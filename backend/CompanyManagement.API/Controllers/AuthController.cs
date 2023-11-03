using System.Net;
using System.Text.Json;

using CompanyManagement.API.Contracts;
using CompanyManagement.API.Exceptions;
using CompanyManagement.API.ViewModels;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;

namespace CompanyManagementService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ApiController
    {
        private readonly IUserLoginService _userLoginService;
        public AuthController(IUserLoginService userLoginService)
        {
            _userLoginService = userLoginService;
        }

        /// <summary>
        /// Login User
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("login")]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(TokenDto))]
        public async Task<IActionResult> Login([FromBody] UserLoginViewModel login)
        {
            return await _userLoginService.LoginUser(login);
        }

        [Authorize]
        [HttpGet("get-user")]
        public async Task<IActionResult> GetUser()
        {
            return await _userLoginService.GetUser();
        }

        [Authorize]
        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _userLoginService.ChangePassword(model);
                return Ok();

            }
            else
            {
                var errors = GetModelStateError();
                throw new ChangePasswordException(JsonSerializer.Serialize(errors));
            }
        }
    }
}