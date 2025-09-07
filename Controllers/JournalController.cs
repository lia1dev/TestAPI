using Microsoft.AspNetCore.Mvc;
using TestAPI.Entities.Journals;
using TestAPI.Entities.Pager;
using TestAPI.Helpers;
using TestAPI.Services.Journals;

namespace TestAPI.Controllers
{
    [ApiController]
    [Route("api.user.journal.[action]")]
    [ApiExplorerSettings(GroupName = "user.journal")]
    [Produces("application/json")]
    public class JournalController : ControllerBase
    {
        private readonly IJournalService _journalService;
        private readonly ILogger<JournalController> _logger;

        public JournalController(IJournalService journalService, ILogger<JournalController> logger)
        {
            _journalService = journalService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult<PageList<Journal>>> GetRange([FromQuery] int skip, [FromQuery] int take, [FromBody] JournalFilter? filter = null)
        {
            var items = await _journalService.SearchByFilterAsync(skip, take, filter);

            return Ok(new { Message = $"Journal range successfully retrieved", journals = items, skip, take });
        }

        [HttpGet]
        public async Task<ActionResult<Journal>> GetSingle([FromQuery] long id)
        {
            var journal = await _journalService.GetJournalByIdAsync(id);

            if (journal == null || journal.IsInvalid())
                return NotFound($"Journal id={id} not found");

            return Ok(journal);
        }
    }
}