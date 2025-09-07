using TestAPI.Entities.Trees;
using TestAPI.Models;

namespace TestAPI.Helpers
{
    public static class TreeMapper
    {
        /// <summary>
        /// Mapping tree to view model
        /// </summary>
        /// <param name="tree">tree entity</param>
        /// <returns></returns>
        public static TreeViewModel? ToViewModel(this Tree tree)
        {
            if (tree == null)
                return null;

            return new TreeViewModel
            {
                Id = tree.Id,
                Name = tree.Name,
                CreatedBy = tree.CreatedBy,
                ModifiedBy = tree.ModifiedBy,
                CreatedOnUtc = tree.CreatedOnUtc,
                ModifiedOnUtc = tree.ModifiedOnUtc,
                Nodes = tree.Nodes.IsAny() ? tree.Nodes.Where(x => x.ParentNodeId == null).Select(n => n.ToViewModel())?.ToList() : new List<TreeNodeViewModel>()
            };
        }

        /// <summary>
        /// Mapping tree node to view model
        /// </summary>
        /// <param name="node">tree node entity</param>
        /// <returns></returns>
        public static TreeNodeViewModel? ToViewModel(this TreeNode node)
        {
            if (node == null)
                return null;

            return new TreeNodeViewModel(node.Name)
            {
                Id = node.Id,
                Name = node.Name,
                ParentNodeId = node.ParentNodeId,
                ParentNode = node.ParentNode?.ToViewModel(),
                TreeId = node.TreeId,
                Tree = node.Tree?.ToViewModel(),
                CreatedBy = node.CreatedBy,
                ModifiedBy = node.ModifiedBy,
                CreatedOnUtc = node.CreatedOnUtc,
                ModifiedOnUtc = node.ModifiedOnUtc,
                Children = node.Children.IsAny() ? node.Children.Select(c => c.ToViewModel()).ToList() : new List<TreeNodeViewModel>()
            };
        }

        /// <summary>
        /// Mapping tree node view model to entity
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static TreeNode? ToEntity(this TreeNodeViewModel model)
        {
            if (model == null)
                return null;

            return new TreeNode
            {
                Id = model.Id,
                Name = model.Name,
                TreeId = model.TreeId,
                ParentNodeId = model.ParentNodeId,
                CreatedBy = model.CreatedBy,
                ModifiedBy = model.ModifiedBy,
                CreatedOnUtc = model.CreatedOnUtc,
                ModifiedOnUtc = model.ModifiedOnUtc,
                Children = model.Children?.Where(x => x.ParentNodeId == null).Select(n => n.ToEntity())?.ToList()
            };
        }

        /// <summary>
        /// Mapping tree view model to entity
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static Tree? ToEntity(this TreeViewModel model)
        {
            if (model == null)
                return null;

            return new Tree
            {
                Id = model.Id,
                Name = model.Name,
                CreatedOnUtc = model.CreatedOnUtc,
                ModifiedOnUtc = model.ModifiedOnUtc,
                Nodes = model.Nodes?.Where(x => x.ParentNodeId != null && x.ParentNodeId.Value == model.Id).Select(n => n.ToEntity()).ToList()
            };
        }
    }
}