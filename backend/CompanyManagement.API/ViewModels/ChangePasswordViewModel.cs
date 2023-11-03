using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CompanyManagement.API.ViewModels
{
    public class ChangePasswordViewModel
    {
        [JsonPropertyName("currentPassword")]
        [DataType(DataType.Password)]
        public required string CurrentPassword { get; set; }

        [JsonPropertyName("newPassword")]
        [DataType(DataType.Password)]
        public required string NewPassword { get; set; }

        [JsonPropertyName("confirmPassword")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public required string ConfirmPassword { get; set; }
    }
}
