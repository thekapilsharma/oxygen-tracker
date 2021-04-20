using Microsoft.AspNetCore.Identity;
using oxygen_tracker.Entities;
using System.Collections.Generic;

namespace oxygen_tracker.Settings.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
    }
}