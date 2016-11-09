using Etrack.Core.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Etrack.Infrastructure.Data.EntitiesConfiguration
{
    public class UserLocationEntityTypeConfiguration : EntityTypeConfiguration<UserLocation>
    {
        public override void Map(EntityTypeBuilder<UserLocation> builder)
        {
            builder.ToTable("UserLocations");

            builder.HasKey(ul => new { ul.UserId, ul.LocationId });

            builder.HasOne(ul => ul.User)
                .WithMany(u => u.AssignedLocations)
                .HasForeignKey(ul => ul.UserId);

            builder.HasOne(ul => ul.Location)
                .WithMany(l => l.AssignedUsers)
                .HasForeignKey(ul => ul.LocationId);
        }
    }
}
