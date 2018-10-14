using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using vtb.Warehouse.Data.Database.Model;

namespace vtb.Warehouse.Data.Database.ModelConfigurations
{
    internal class BuildingEntityConfiguration : IEntityTypeConfiguration<Building>
    {
        public void Configure(EntityTypeBuilder<Building> builder)
        {
            builder.Property(b => b.Label).HasMaxLength(30);

            builder
                .HasMany(b => b.Racks)
                .WithOne(r => r.Building)
                .IsRequired();
        }
    }

    internal class RackEntityConfiguration : IEntityTypeConfiguration<Rack>
    {
        public void Configure(EntityTypeBuilder<Rack> builder)
        {
            builder.Property(r => r.BuildingId).IsRequired();
            builder.Property(r => r.Order).IsRequired();

            builder.HasIndex(r => new { r.BuildingId, r.Order }).IsUnique();

            builder
                .HasMany(r => r.Shelves)
                .WithOne(s => s.Rack)
                .IsRequired();
        }
    }

    internal class ShelfEntityConfiguration : IEntityTypeConfiguration<Shelf>
    {
        public void Configure(EntityTypeBuilder<Shelf> builder)
        {
            builder.Property(s => s.RackId).IsRequired();
            builder.Property(s => s.Width).IsRequired();
            builder.Property(s => s.Height).IsRequired();
            builder.Property(s => s.Depth).IsRequired();

            builder
                .HasMany(s => s.StorageUnits)
                .WithOne(su => su.Shelf);
        }
    }

    internal class StorageUnitEntityConfiguration : IEntityTypeConfiguration<StorageUnit>
    {
        public void Configure(EntityTypeBuilder<StorageUnit> builder)
        {
            builder.Property(su => su.Quantity).HasDefaultValue(1).IsRequired();
            builder.Property(su => su.ShelfId).IsRequired();
            builder.Property(su => su.UnitId).IsRequired();
            builder.HasOne(su => su.Unit).WithMany().IsRequired();
        }
    }

    internal class UnitEntityConfiguration : IEntityTypeConfiguration<Unit>
    {
        public void Configure(EntityTypeBuilder<Unit> builder)
        {
            builder.Property(u => u.Label).HasMaxLength(128).IsRequired();
            builder.Property(u => u.Weight).IsRequired();
            builder.Property(u => u.Height).IsRequired();
            builder.Property(u => u.Depth).IsRequired();

            builder.Property(u => u.Weight).IsRequired();
        }
    }
}
