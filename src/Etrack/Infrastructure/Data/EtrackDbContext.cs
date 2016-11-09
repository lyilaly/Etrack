using Etrack.Core.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Etrack.Data
{
    public class EtrackDbContext : IdentityDbContext<User>
    {
        public DbSet<Location> Locations { get; set; }

        public EtrackDbContext(DbContextOptions<EtrackDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            builder.Entity<UserLocation>()
                .HasKey(ul => new { ul.UserId, ul.LocationId });


            builder.Entity<UserLocation>()
                .HasOne(ul => ul.User)
                .WithMany(u => u.AssignedLocations)
                .HasForeignKey(ul => ul.UserId);

            builder.Entity<UserLocation>()
                .HasOne(ul => ul.Location)
                .WithMany(l => l.AssignedUsers)
                .HasForeignKey(ul => ul.LocationId);
        }
    }
}
