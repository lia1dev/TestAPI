using Microsoft.AspNetCore.Mvc;
using TestAPI.Entities;
using TestAPI.Helpers;
using TestAPI.Services.Trees;

namespace TestAPI.Controllers
{
    [ApiController]
    [Route("api.user.tree.[action]")]
    [ApiExplorerSettings(GroupName = "user.tree")]
    [Produces("application/json")]
    public class TreeController : ControllerBase
    {
        private readonly ITreeNodeService _treeNodeService;
        private readonly ILogger<TreeController> _logger;

        public TreeController(ITreeNodeService treeNodeService, ILogger<TreeController> logger)
        {
            _treeNodeService = treeNodeService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult> Get([FromQuery] string treeName)
        {
            if (string.IsNullOrEmpty(treeName))
            {
                var errorMsg = $"{nameof(TreeController)} GetTree - Tree name is required";
                _logger.LogError(errorMsg);

                return BadRequest(errorMsg);
            }

            var message = $"Tree {treeName} successfully found";

            var tree = await _treeNodeService.GetTreeByNameAsync(treeName);

            if (tree.IsInvalid())
            {
                var data = new Tree() { Name = treeName };
                tree = await _treeNodeService.CreateTreeAsync(data);

                if (tree.IsInvalid()) 
                {
                    var errorMsg = $"{nameof(TreeController)} GetTree - Tree {treeName} create failed";
                    _logger.LogError(errorMsg);

                    return BadRequest(errorMsg);
                }

                message = $"Tree {treeName} not found, it was created successfully!";
            }

            var model = tree.ToViewModel();

            return Ok(new { Message = message, Tree = model });
        }
    }
}