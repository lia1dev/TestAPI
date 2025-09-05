using System.ComponentModel.DataAnnotations;
using TestAPI.Common;

namespace TestAPI.Entities
{
    /// <summary>
    /// Tree entity
    /// </summary>
    public class Tree : BaseEntity
    {
        /// <summary>
        /// Tree name required field
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Tree nodes collection
        /// </summary>
        public ICollection<TreeNode> Nodes { get; set; } = new List<TreeNode>();
    }
}