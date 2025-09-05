using TestAPI.Common;

namespace TestAPI.Models
{
    /// <summary>
    /// Tree view model
    /// </summary>
    public class TreeViewModel : BaseEntity
    {
        /// <summary>
        /// Tree name required field
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Tree nodes collection
        /// </summary>
        public ICollection<TreeNodeViewModel> Nodes { get; set; } = new List<TreeNodeViewModel>();
    }
}