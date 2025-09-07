using System.ComponentModel.DataAnnotations;
using TestAPI.Entities.Base;

namespace TestAPI.Entities.Journals
{
    public class Journal : BaseEntity
    {
        [Required]
        public long EventId { get; set; }

        [Required]
        public required string Text { get; set; }
    }
}