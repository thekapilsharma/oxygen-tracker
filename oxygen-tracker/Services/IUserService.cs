using oxygen_tracker.Models;
using oxygen_tracker.Settings.Models;
using System.Threading.Tasks;

namespace oxygen_tracker.Controllers.Services
{
    public interface IUserService
    {
        Task<UserDetail> GetUserInfoAsync(string phoneNumber);

        Task<AuthenticationModel> RegisterAsync(RegisterModel model);

        //Task<AuthenticationModel> RefreshTokenAsync(string jwtToken);

        //bool RevokeToken(string token);
    }
}