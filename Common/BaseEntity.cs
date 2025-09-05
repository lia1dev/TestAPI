using System.ComponentModel.DataAnnotations;

namespace TestAPI.Common
{
    public abstract class BaseEntity : IBaseEntity
    {
        [Key]
        public int Id { get; set; }
        public string? CreatedBy { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? CreatedOnUtc { get; set; } = DateTime.UtcNow;
        public DateTime? ModifiedOnUtc { get; set; } = DateTime.UtcNow;
    }
}