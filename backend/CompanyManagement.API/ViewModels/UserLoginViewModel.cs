using System.Text.Json.Serialization;

namespace CompanyManagement.API.ViewModels
{
    public class UserLoginViewModel
    {
        [JsonPropertyName("email")]
        public string Email { get; set; }
        [JsonPropertyName("password")]
        public string Password { get; set; }
    }
}
