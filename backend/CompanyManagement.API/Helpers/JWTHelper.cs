using Newtonsoft.Json.Linq;

using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;

namespace CompanyManagement.API.Helpers
{
    public class JWTHelper
    {
        public static Dictionary<string, string> Decode(string jwt)
        {
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(jwt);
            var dict = new Dictionary<string, string>();

            // decode the claims
            foreach (var claim in token.Claims)
            {
                if (claim.Type.ToLower().EndsWith("claims"))
                {
                    JObject c = JObject.Parse(claim.Value);
                    foreach (var v in c)
                        dict.Add(v.Key, v.Value.ToString());
                }
                else
                    dict.Add(claim.Type, claim.Value);
            }
            return dict;
        }
    }
}
