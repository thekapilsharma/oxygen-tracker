using Microsoft.AspNetCore.Mvc;
using oxygen_tracker.Models;
using oxygen_tracker.Services.Interface;
using System.Threading.Tasks;

namespace oxygen_tracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserLocationController : ControllerBase
    {
        private readonly ILocationService _LocationService;

        public UserLocationController(ILocationService locationService)
        {
            _LocationService = locationService;
        }

        [HttpPost]
        public async Task<IActionResult> AddLocation(UserLocation model)
        {
            var result = await _LocationService.AddLocationAsync(model);
            //if (result.ErrorCodes == Constants.DefaultValues.ErrorCodes.None) SetRefreshTokenInCookie(result.RefreshToken);
            return Ok(result);
        }

        [HttpGet("{pageNumber}/{pageSize}")]
        public async Task<IActionResult> GetLocation(int pageNumber,int pageSize)
        {
            var result = await _LocationService.GetLocationAsync(pageSize,pageNumber);
            //if (result.ErrorCodes == Constants.DefaultValues.ErrorCodes.None) SetRefreshTokenInCookie(result.RefreshToken);
            return Ok(result);
        }
    }
}