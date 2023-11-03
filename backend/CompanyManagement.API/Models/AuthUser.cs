using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompanyManagement.API.Models
{
    [Table("Users")]
    public class AuthUser : IEntity
    {
        [Key]
        public Guid Id { get; set; }
        [StringLength(100)]
        public required string FirstName { get; set; }
        [StringLength(100)]
        public required string LastName { get; set; }
        [StringLength(100)]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string Salt { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? ExpiresAt { get; set; }
    }
}
