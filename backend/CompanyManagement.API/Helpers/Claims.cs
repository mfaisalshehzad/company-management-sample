using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;

using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace CompanyManagement.API.Helpers
{
    public static class Claims
    {
        public static Guid GetUserId(this HttpContext context)
        {
            return GetClaimsValue(context, ClaimTypes.NameIdentifier);
        }

        public static string GetAccessToken(this HttpContext context)
        {
            string result = string.Empty;
            if (context != null)
            {
                var claim = context.User.FindFirst("access_token");
                
                if (claim != null)
                {
                    result = claim.Value;
                }
            }

            return result;
        }

        public static string GetRemoteIPAddr(this HttpContext context)
        {
            if (context == null) return string.Empty;

            try
            {
                var header = context.Request.Headers.SingleOrDefault(s => s.Key == "remoteIpAddress");
                if (header.Key != null) return header.Value;

                var _reqHeaders = context.Request.Headers;
                var _forwardedFor = _reqHeaders[ForwardedHeadersDefaults.XForwardedForHeaderName];
                var ipAddr = _forwardedFor.ToString();

                if (string.IsNullOrEmpty(ipAddr))
                {
                    ipAddr = context.Connection.RemoteIpAddress?.ToString();
                }

                return ipAddr;
            }
            catch { return string.Empty; }
        }

        private static Guid GetClaimsValue(HttpContext context, string claimName)
        {
            Guid result = Guid.Empty;
            if (context != null)
            {
                var claim = context.User.FindFirst(claimName);
                
                if (claim != null && Guid.TryParse(claim.Value, out result))
                {
                    return result;
                }

                var header = context.Request.Headers.SingleOrDefault(s => s.Key.ToLower() == claimName.ToLower());
                if (header.Key != null && Guid.TryParse(header.Value, out result))
                {
                    return result;
                }
            }

            return result;
        }

        private static Guid GetClaimsValueHeaderFirst(HttpContext context, string claimName)
        {
            Guid result = Guid.Empty;
            if (context != null)
            {
                var header = context.Request.Headers.SingleOrDefault(s => s.Key.ToLower() == claimName.ToLower());
                if (header.Key != null && Guid.TryParse(header.Value, out result))
                {
                    return result;
                }

                var claim = context.User.FindFirst(claimName);
                if (claim != null && Guid.TryParse(claim.Value, out result))
                {
                    return result;
                }
            }

            return result;
        }
    }
}
