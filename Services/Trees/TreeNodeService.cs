using Microsoft.EntityFrameworkCore;
using TestAPI.Entities.Trees;
using TestAPI.Helpers;
using TestAPI.Repositories;

namespace TestAPI.Services.Trees
{
    public class TreeNodeService : ITreeNodeService
    {
        private IBaseRepository<Tree> _trees;
        private IBaseRepository<TreeNode> _treeNodes { get; set; }

        public TreeNodeService(IBaseRepository<Tree> trees, IBaseRepository<TreeNode> treeNodes)
        {
            _trees = trees;
            _treeNodes = treeNodes;
        }

        public async Task<Tree> GetTreeByNameAsync(string name)
        {
            ArgumentException.ThrowIfNullOrEmpty(name);

            var tree = await _trees.Table.Include(t => t.Nodes).FirstOrDefaultAsync(x => x.Name.ToLower().Trim() == name.ToLower().Trim());

            return tree;
        }

        public async Task<Tree> CreateTreeAsync(Tree data)
        {
            ArgumentNullException.ThrowIfNull(data);

            var item = await _trees.CreateAsync(data);

            return item;
        }

        public async Task<TreeNode> GetTreeNodeByIdAsync(long id)
        {
            var item = await _treeNodes.GetByIdAsync(id);

            return item;
        }

        public async Task<IList<TreeNode>> GetRootNodesByTreeIdAsync(long treeId)
        {
            var items = await _treeNodes.Table.Where(x => x.TreeId == treeId && x.ParentNodeId == null).Include(t => t.Children).ToListAsync();

            return items;
        }

        public async Task<TreeNode> GetTreeNodeWithChildrenAsync(long id)
        {
            var item = await _treeNodes.Table.Where(x => x.Id == id).Include(t => t.Children).FirstOrDefaultAsync();

            return item;
        }

        public async Task<TreeNode> CreateTreeNodeAsync(TreeNode data)
        {
            ArgumentNullException.ThrowIfNull(data);

            var item = await _treeNodes.CreateAsync(data);

            return item;
        }
        
        public async Task<TreeNode> UpdateTreeNodeAsync(TreeNode data)
        {
            ArgumentNullException.ThrowIfNull(data);

            var item = await _treeNodes.UpdateAsync(data);

            return item;
        }

        public async Task DeleteTreeNodeAsync(long id)
        {
            var childToRemove = await _treeNodes.TableNoTracking.Where(x => x.ParentNodeId == id).Select(c => c.Id).ToArrayAsync();
            
            if (childToRemove.IsAny())
            {
                await _treeNodes.DeleteRangeAsync(childToRemove);
            }

            await _treeNodes.DeleteAsync(id);
        }
    }
}