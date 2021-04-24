using oxygen_tracker.Entities;
using oxygen_tracker.Models;
using oxygen_tracker.Settings.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

namespace oxygen_tracker.Services.Interface
{
    public interface IJwtTokenService
    {
        RefreshToken CreateRefreshToken();

        Task<JwtSecurityToken> CreateJwtTokenAsync(ApplicationUser user);

        Task<AuthenticationModel> RefreshTokenAsync(string jwtToken);

        bool RevokeToken(string token);
    }
}