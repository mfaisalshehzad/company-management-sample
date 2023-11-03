using System;
using System.Text.Json.Serialization;

namespace CompanyManagement.API.ViewModels
{
    public class TokenDto
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }
        [JsonPropertyName("scope")]
        public string Scope { get; set; }
        [JsonPropertyName("expires_in")]
        public long ExpiresIn { get; set; }
        [JsonPropertyName("token_type")]
        public string TokenType { get; set; }
        [JsonPropertyName("password_validity_days")]
        public int? PasswordValidityDays { get; set; }
    }

    public class ForgotPasswordTokenDTO
    {
        [JsonPropertyName("token")]
        public string Token { get; set; }
        [JsonPropertyName("email_username")]
        public string EmailOrUsername { get; set; }
        [JsonPropertyName("expires_in")]
        public int ExpiresIn { get; set; }
    }

    public class ValidatedTokenDTO
    {
        public string Token { get; set; }
        public bool IsValid { get; set; }
        public string Subject { get; set; }
        public string Claims { get; set; }
        public string Scope { get; set; }
        public long Expiry { get; set; }
        public Guid userId { get; set; }

    }
}
