using TestAPI.Entities.Trees;

namespace TestAPI.Services.Trees
{
    public interface ITreeNodeService
    {
        Task<Tree> GetTreeByNameAsync(string name);
        Task<Tree> CreateTreeAsync(Tree data);
        Task<TreeNode> GetTreeNodeByIdAsync(long id);
        Task<IList<TreeNode>> GetRootNodesByTreeIdAsync(long treeId);
        Task<TreeNode> GetTreeNodeWithChildrenAsync(long id);
        Task<TreeNode> CreateTreeNodeAsync(TreeNode data);
        Task<TreeNode> UpdateTreeNodeAsync(TreeNode data);
        Task DeleteTreeNodeAsync(long id);
    }
}