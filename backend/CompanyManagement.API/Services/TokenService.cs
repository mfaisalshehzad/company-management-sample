using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

using CompanyManagement.API.Contracts;
using CompanyManagement.API.Models;
using CompanyManagement.API.ViewModels;

using Microsoft.IdentityModel.Tokens;

namespace CompanyManagement.API.Services
{
    public class TokenService : ITokenService
    {
        private readonly ILogger _logger;
        private readonly string ClaimsType = "https://endpoint.ezyhaul.com/claims";
        private readonly IConfiguration _config;

        private readonly string? _secret;
        private readonly string? _issuer;
        private readonly string? _audience;
        private readonly int _defaultExpiry;

        public TokenService(IConfiguration config, ILoggerFactory loggerFactory)
        {
            _config = config;
            _logger = loggerFactory.CreateLogger("Default");
            _secret = _config.GetValue<string>("Jwt:Secret");
            _issuer = _config.GetValue<string>("Jwt:Issuer");
            _audience = _config.GetValue<string>("Jwt:Audience");
            _defaultExpiry = _config.GetValue("Jwt:DefaultExpiry", 120);
        }

        public async Task<TokenDto> CreateAuthToken(AuthUser user)
        {
            string defaultScopes = "openid email firstname lastname";
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secret));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.GivenName,user.FirstName),
                new Claim(JwtRegisteredClaimNames.FamilyName,user.LastName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var token = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_defaultExpiry),
                signingCredentials: signingCredentials
            );
            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            return new TokenDto()
            {
                AccessToken = jwtToken,
                Scope = defaultScopes,
                ExpiresIn = _defaultExpiry * 60,
                TokenType = "Bearer"
            };
        }
    }
}
