namespace TestAPI.Models
{
    /// <summary>
    /// Tree view model
    /// </summary>
    public class TreeViewModel : BaseViewModel
    {
        /// <summary>
        /// Tree name required field
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// Tree nodes collection
        /// </summary>
        public IList<TreeNodeViewModel>? Nodes { get; set; }
    }
}