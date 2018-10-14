using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Microsoft.EntityFrameworkCore;
using vtb.Core.Utils;
using vtb.Warehouse.Data.Database.Model;
using vtb.Warehouse.Data.Database.ModelConfigurations;

namespace vtb.Warehouse.Data.Database
{
    public class WarehouseContext : DbContext
    {
        public DbSet<Building> Buildings { get; set; }
        public DbSet<Rack> Racks { get; set; }
        public DbSet<Shelf> Shelves { get; set; }

        public DbSet<StorageUnit> StorageUnits { get; set; }
        public DbSet<Unit> Units { get; set; }

        protected const string ModelConfigurationsNamespace = "vtb.Warehouse.Data.ModelConfigurations";


        public WarehouseContext(DbContextOptions<WarehouseContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new BuildingEntityConfiguration());
            modelBuilder.ApplyConfiguration(new RackEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ShelfEntityConfiguration());
            modelBuilder.ApplyConfiguration(new StorageUnitEntityConfiguration());
            modelBuilder.ApplyConfiguration(new UnitEntityConfiguration());

            Seed(modelBuilder);
        }

        private void Seed(ModelBuilder modelBuilder)
        {
            using (var seed = new InefficientStorageWarehouseSeed())
                seed.Seed(modelBuilder);
        }
    }
}
