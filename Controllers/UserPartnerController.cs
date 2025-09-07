using Microsoft.AspNetCore.Mvc;
using TestAPI.Entities;

namespace TestAPI.Controllers
{
    [ApiController]
    [Route("api.user.partner.[action]")]
    [ApiExplorerSettings(GroupName = "user.partner")]
    [Produces("application/json")]
    public class UserPartnerController : ControllerBase
    {
        [HttpPost]
        public ActionResult<TokenInfo> RememberMe([FromQuery] string code)
        {
            if (string.IsNullOrEmpty(code))
                return BadRequest("Code is required");

            var token = Convert.ToBase64String(Guid.NewGuid().ToByteArray());

            return Ok(new TokenInfo { Token = token });
        }
    }
}