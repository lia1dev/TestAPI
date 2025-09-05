using TestAPI.Common;
using TestAPI.Entities;

namespace TestAPI.Models
{
    /// <summary>
    /// Tree node view model
    /// </summary>
    public class TreeNodeViewModel : BaseEntity
    {
        /// <summary>
        /// Node name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Parent node ID (root = null)
        /// </summary>
        public int? ParentId { get; set; }

        /// <summary>
        /// Parent node (root = null)
        /// </summary>
        public TreeNodeViewModel Parent { get; set; }

        /// <summary>
        /// Tree ID
        /// </summary>
        public int TreeId { get; set; }

        /// <summary>
        /// Tree to which the node belongs
        /// </summary>
        public Tree Tree { get; set; }

        /// <summary>
        /// Child nodes collection
        /// </summary>
        public ICollection<TreeNodeViewModel> Children { get; set; } = new List<TreeNodeViewModel>();

        /// <summary>
        /// Сonstructor for required node name field
        /// </summary>
        /// <param name="name">node name</param>
        /// <exception cref="ArgumentException"></exception>
        public TreeNodeViewModel(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("The node name must not be empty", nameof(name));

            Name = name;
        }
    }
}