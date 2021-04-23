using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using oxygen_tracker.Models;

namespace oxygen_tracker.Settings.Models.Contexts
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<UserLocation> UserLocation { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UserLocation>()
             .Property(bs => bs.CreatedOn)
             .HasDefaultValueSql("getdate()");
            base.OnModelCreating(builder);
        }
    }
}