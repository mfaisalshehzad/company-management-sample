using CompanyManagement.API.Contracts;
using CompanyManagement.API.Exceptions;
using CompanyManagement.API.ViewModels;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;

using System.Text.Json;

using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CompanyManagementService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CompanyController : ApiController
    {
        private readonly ICompanyService _companyService;
        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        /// <summary>
        /// Get companies list
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("get-list")]

        public async Task<IActionResult> GetList([FromBody] CompanyFilterViewModel model)
        {
            var result = await _companyService.GetList(model);
            return new OkObjectResult(result);
        }

        /// <summary>
        /// Get companies list for the dropdown binding
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]

        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _companyService.Get(id);
            return new OkObjectResult(result);
        }


        /// <summary>
        /// Get companies list for the dropdown binding
        /// </summary>
        /// <returns></returns>
        [HttpGet("get-all")]

        public async Task<IActionResult> GetAll()
        {
            var result = await _companyService.GetAll();
            return new OkObjectResult(result);
        }

        /// <summary>
        /// Create new company
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("create-or-update")]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(CompanyViewModel))]
        public async Task<IActionResult> CreateOrUpdate([FromBody] UpdateCompanyViewModel model)
        {
            if (ModelState.IsValid)
            {
                return new OkObjectResult(await _companyService.CreateOrUpdateCompany(model));
            }
            else
            {
                var errors = GetModelStateError();
                throw new CreateUpdateCompanyException(JsonSerializer.Serialize(errors));
            }

        }
    }
}