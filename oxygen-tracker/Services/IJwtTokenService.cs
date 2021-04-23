using System.Threading.Tasks;
using oxygen_tracker.Entities;
using oxygen_tracker.Settings.Models;
using System.IdentityModel.Tokens.Jwt;
using oxygen_tracker.Models;

namespace oxygen_tracker.Services
{
    public interface IJwtTokenService
    {
        RefreshToken CreateRefreshToken();
        Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user);
        Task<AuthenticationModel> RefreshTokenAsync(string jwtToken);
        bool RevokeToken(string token);
    }
}
