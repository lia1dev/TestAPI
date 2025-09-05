using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TestAPI.Common;

namespace TestAPI.Entities
{
    /// <summary>
    /// Tree node entity
    /// </summary>
    public class TreeNode : BaseEntity
    {
        /// <summary>
        /// Node name required field
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Foreign key to parent node (for root - null)
        /// </summary>
        public int? ParentId { get; set; }

        [ForeignKey("ParentId")]
        public TreeNode Parent { get; set; }

        /// <summary>
        /// Foreign key to Tree ID
        /// </summary>
        [Required]
        public int TreeId { get; set; }

        [ForeignKey("TreeId")]
        public Tree Tree { get; set; }

        /// <summary>
        /// Child nodes collection
        /// </summary>
        public ICollection<TreeNode> Children { get; set; } = new List<TreeNode>();
    }
}