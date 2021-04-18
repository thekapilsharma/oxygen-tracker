using Microsoft.AspNetCore.Identity;

namespace oxygen_tracker.Settings.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}