using System.ComponentModel.DataAnnotations;

namespace TestAPI.Entities.Base
{
    public abstract class BaseEntity : IBaseEntity
    {
        [Key]
        public long Id { get; set; }
        public string? CreatedBy { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? CreatedOnUtc { get; set; } = DateTime.UtcNow;
        public DateTime? ModifiedOnUtc { get; set; } = DateTime.UtcNow;
    }
}