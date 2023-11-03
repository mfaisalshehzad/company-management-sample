using CompanyManagement.API.Enums;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompanyManagement.API.Models
{
    [Table("Companies")]
    public class Company : IEntity
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "CompanyNo must be a unique and non-negative number.")]
        public int CompanyNo { get; set; }

        [StringLength(50, ErrorMessage = "The compnay name length must be within {0} characters.")]
        [Required(ErrorMessage = "Company is required")]
        public required string CompanyName { get; set; }
        [StringLength(100, ErrorMessage = "The compnay name length must be within {0} characters.")]
        public IndustryType Industry { get; set; }
        [Range(1, 1000000, ErrorMessage = "Number of employees must be betwen 1 and 1,000,000")]
        public int NumberOfEmployees { get; set; }
        [StringLength(50, ErrorMessage = "The city length must be within {0} characters")]
        [RegularExpression("^[a-zA-Z-]*$", ErrorMessage = "Only alphabetic characters and hyphens are allowed.")]
        public required string City { get; set; }
        public Guid? ParentCompanyId { get; set; }
        public int CompanyLevel { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }

        [ForeignKey("ParentCompanyId")]
        public virtual Company? ParentCompany { get; set; }
    }
}
