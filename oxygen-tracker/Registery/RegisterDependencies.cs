using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using oxygen_tracker.Services;
using oxygen_tracker.Services.Interface;
using oxygen_tracker.Settings.Models;
using oxygen_tracker.Settings.Models.Contexts;

namespace oxygen_tracker.Registery
{
    public static class RegisterDependencies
    {
        public static IServiceCollection RegisterLocalDependecies(this IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddScoped<IVerification, Verification>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ILocationService, LocationService>();
            services.AddScoped<IJwtTokenService, JwtTokenService>();
            return services;
        }
    }
}