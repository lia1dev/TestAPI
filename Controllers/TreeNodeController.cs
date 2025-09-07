using Microsoft.AspNetCore.Mvc;
using TestAPI.Entities;
using TestAPI.Helpers;
using TestAPI.Services.Trees;

namespace TestAPI.Controllers
{
    [ApiController]
    [Route("api.user.tree.node.[action]")]
    [ApiExplorerSettings(GroupName = "user.tree.node")]
    [Produces("application/json")]
    public class TreeNodeController : ControllerBase
    {
        private readonly ITreeNodeService _treeNodeService;
        private readonly ILogger<TreeNodeController> _logger;

        public TreeNodeController(ITreeNodeService treeNodeService, ILogger<TreeNodeController> logger)
        {
            _treeNodeService = treeNodeService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromQuery] string treeName, [FromQuery] long? parentNodeId, [FromQuery] string nodeName)
        {
            if (string.IsNullOrEmpty(treeName))
                return BadRequest("Tree name is required");
            if (string.IsNullOrEmpty(nodeName))
                return BadRequest("Node name is required");

            var tree = await _treeNodeService.GetTreeByNameAsync(treeName);

            if (tree.IsInvalid())
                return NotFound($"Tree {treeName} not found");

            IList<TreeNode> siblings;

            if (parentNodeId.GetValueOrDefault() > 0)
            {
                var parentNode = await _treeNodeService.GetTreeNodeWithChildrenAsync(parentNodeId.GetValueOrDefault());
                if (parentNode.IsInvalid())
                    return NotFound($"Parent Node id={parentNodeId} not found");

                siblings = parentNode.Children ?? new List<TreeNode>();
            }
            else
            {
                siblings = await _treeNodeService.GetRootNodesByTreeIdAsync(tree.Id) ?? new List<TreeNode>();
            }

            //Check for unique node name among siblings
            if (siblings.Any(n => string.Equals(n.Name, nodeName, StringComparison.OrdinalIgnoreCase)))
                return BadRequest($"Node name {nodeName} must be unique among siblings");

            var item = new TreeNode()
            {
                Name = nodeName,
                TreeId = tree.Id,
                ParentNodeId = parentNodeId,
                CreatedOnUtc = DateTime.UtcNow,
                ModifiedOnUtc = DateTime.UtcNow
            };

            var node = await _treeNodeService.CreateTreeNodeAsync(item);

            if (node.IsInvalid())
                return BadRequest($"Tree node {nodeName} not created");

            return Ok(new { Message = $"Tree node {nodeName} was successfully created!", NodeName = nodeName, ParentNodeId = parentNodeId });
        }

        [HttpPost]
        public async Task<ActionResult> Delete([FromQuery] long nodeId)
        {
            var node = await _treeNodeService.GetTreeNodeByIdAsync(nodeId);

            if (node.IsInvalid())
                return NotFound($"Node id={nodeId} not found");

            await _treeNodeService.DeleteTreeNodeAsync(nodeId);

            return Ok(new { Message = $"Tree node id={nodeId} was successfully deleted!", NodeId = nodeId });
        }

        [HttpPost]
        public async Task<ActionResult> Rename([FromQuery] long nodeId, [FromQuery] string newNodeName)
        {
            if (string.IsNullOrEmpty(newNodeName))
                return BadRequest("New node name is required");

            var node = await _treeNodeService.GetTreeNodeByIdAsync(nodeId);

            if (node.IsInvalid())
                return NotFound($"Node id={nodeId} not found");

            IList<TreeNode> siblings;

            var parentNodeId = node.ParentNodeId.GetValueOrDefault();

            if (parentNodeId > 0)
            {
                var parentNode = await _treeNodeService.GetTreeNodeWithChildrenAsync(parentNodeId);
                if (parentNode.IsInvalid())
                    return NotFound($"Parent Node id={parentNodeId} not found");

                siblings = parentNode.Children ?? new List<TreeNode>();
            }
            else
            {
                siblings = await _treeNodeService.GetRootNodesByTreeIdAsync(node.TreeId) ?? new List<TreeNode>();
            }
            
            //Check for unique node name among siblings
            if (siblings.Any(n => n.Id != nodeId && string.Equals(n.Name, newNodeName, StringComparison.OrdinalIgnoreCase)))
                return BadRequest($"Node name {newNodeName} must be unique among siblings");

            node.Name = newNodeName;

            var item = await _treeNodeService.UpdateTreeNodeAsync(node);
            
            return Ok(new { Message = $"Tree node id={nodeId} was successfully updated!", NodeId = nodeId, NewNodeName = newNodeName });
        }
    }
}