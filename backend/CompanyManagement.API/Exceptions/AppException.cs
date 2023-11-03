using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;

namespace CompanyManagement.API.Exceptions
{
    public abstract class AppException: Exception
    {
        public string Code { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string ErrorMessage { get; set; }
        public AppException(string code, HttpStatusCode statusCode)
        {
            Code = code;
            StatusCode = statusCode;
            ErrorMessage = JsonConvert.SerializeObject(new { code = code });
        }
        public AppException(string code, HttpStatusCode statusCode, string message)
        {
            Code = code;
            StatusCode = statusCode;
            ErrorMessage = JsonConvert.SerializeObject(new { code = code, message = message });
        }
        public AppException(string code) : base(JsonConvert.SerializeObject(new { code = code }))
        {
            Code = code;
            StatusCode = HttpStatusCode.BadRequest;
            ErrorMessage = JsonConvert.SerializeObject(new { code = code, message = base.Message });
        }
    }
}
