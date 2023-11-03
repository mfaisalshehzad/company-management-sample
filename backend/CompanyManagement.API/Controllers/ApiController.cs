using Microsoft.AspNetCore.Mvc;

namespace CompanyManagementService.Controllers
{
    public abstract class ApiController : ControllerBase
    {
        protected IEnumerable<string> GetModelStateError()
        {
            return ModelState.Keys.SelectMany(k => ModelState[k].Errors.Select(e => e.ErrorMessage));
        }
    }
}



