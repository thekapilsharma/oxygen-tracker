using oxygen_tracker.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using static oxygen_tracker.Constants.DefaultValues;

namespace oxygen_tracker.Services.Interface
{
    public interface ILocationService
    {
        public Task<ErrorCodes> AddLocationAsync(UserLocation model);

        public Task<List<UserLocation>> GetLocationAsync(int count, int pageSize);
    }
}