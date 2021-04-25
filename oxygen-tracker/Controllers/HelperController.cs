using Microsoft.AspNetCore.Mvc;
using oxygen_tracker.Services.Interface;
using System.Threading.Tasks;

namespace oxygen_tracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HelperController : ControllerBase
    {
        private readonly IHelperService _helperService;

        public HelperController(IHelperService helperService)
        {
            _helperService = helperService;
        }

        [HttpGet("{pincode}")]
        public async Task<ActionResult> GetUser(string pincode)
        {
            var userDetail = await _helperService.GetStateNameAsync(pincode);
            return Ok(userDetail);
        }
    }
}