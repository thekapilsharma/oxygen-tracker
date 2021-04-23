using Microsoft.AspNetCore.Identity;
using oxygen_tracker.Constants;
using oxygen_tracker.Models;
using oxygen_tracker.Services.Interface;
using oxygen_tracker.Settings.Models;
using oxygen_tracker.Settings.Models.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace oxygen_tracker.Services
{
    public class LocationService : ILocationService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public LocationService(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            this._dbContext = dbContext;
            this._userManager = userManager;
        }
        public async Task<DefaultValues.ErrorCodes> AddLocationAsync(UserLocation model)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(model.UserGuid);
                model.User = user;
                model.IsActive = true;
                await _dbContext.UserLocation.AddAsync(model);
                await _dbContext.SaveChangesAsync();
                return DefaultValues.ErrorCodes.None;
            }
            catch (Exception e)
            {

                return DefaultValues.ErrorCodes.SomethingWentWrong;
            }
        }

        public Task<List<UserLocation>> GetLocationAsync(int count, int pageSize)
        {
            var list = _dbContext.UserLocation.ToList();
            throw new NotImplementedException();
        }
    }
}