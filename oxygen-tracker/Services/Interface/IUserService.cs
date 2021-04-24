using oxygen_tracker.Models;
using System.Threading.Tasks;

namespace oxygen_tracker.Services.Interface
{
    public interface IUserService
    {
        Task<UserDetail> GetUserInfoAsync(string phoneNumber);

        Task<AuthenticationModel> RegisterAsync(RegisterModel model);
    }
}