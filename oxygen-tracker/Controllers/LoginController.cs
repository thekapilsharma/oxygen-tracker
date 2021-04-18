using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace oxygen_tracker.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetSecuredData()
        {
            return Ok("All good");
        }

        [HttpPost]
        [Authorize(Roles = "Administration")]
        public async Task<IActionResult> PostSecuredData()
        {
            return Ok("Posted correctly");
        }
    }
}