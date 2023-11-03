namespace CompanyManagement.API.Models
{
    public interface IEntity
    {
        public Guid Id { get; set; }
        Guid CreatedBy { get; set; }
        DateTime CreatedAt { get; set; }
        Guid? UpdatedBy { get; set; }
        DateTime? UpdatedAt { get; set; }
    }
}
