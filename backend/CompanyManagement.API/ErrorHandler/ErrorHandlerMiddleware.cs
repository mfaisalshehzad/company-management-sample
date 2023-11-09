using CompanyManagement.API.Exceptions;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;

namespace CompanyManagement.API.ErrorHandler
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly ILogger _logger;

        public ErrorHandlerMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<ErrorHandlerMiddleware>();
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, context.Request.GetDisplayUrl() ?? "");
                AppException? ex2 = ex as AppException;
                if (ex2 != null)
                {
                    context.Response.StatusCode = (int)ex2.StatusCode;
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(ex2.ErrorMessage);
                }
                else
                {
                    context.Response.StatusCode = 500;
                    context.Response.ContentType = "application/json";
                    var value = new
                    {
                        code = "ERROR_OCCURED",
                        message = ex.Message
                    };
                    await context.Response.WriteAsync(JsonConvert.SerializeObject(value));
                }
            }
        }
    }
}
