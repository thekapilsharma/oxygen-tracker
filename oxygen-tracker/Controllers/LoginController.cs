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
        public IActionResult GetSecuredData()
        {
            return Ok("This Secured Data is available only for Authenticated Users.");
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public IActionResult PostSecuredData()
        {
            return Ok("This Secured Data is available only for Administrators.");
        }
    }
}