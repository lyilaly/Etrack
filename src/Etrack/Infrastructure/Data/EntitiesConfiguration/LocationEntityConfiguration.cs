using Etrack.Core.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Etrack.Infrastructure.Data.EntitiesConfiguration
{
    public class LocationEntityConfiguration : EntityTypeConfiguration<Location>
    {
        public override void Map(EntityTypeBuilder<Location> builder)
        {
            builder.ToTable("Locations");

            builder.HasKey(l => l.Id);

            builder.Property(l => l.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasIndex(l => l.Name)
                .ForSqlServerHasName("IX_Location_LocationName");
        }
    }
}
