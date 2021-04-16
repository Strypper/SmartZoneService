using Microsoft.EntityFrameworkCore;

namespace SmartZone.Entities
{
    public class SmartZoneContext : DbContext
    {
        public SmartZoneContext(DbContextOptions<SmartZoneContext> options) : base(options) { }

        public virtual DbSet<SmartZone> SmartZones { get; set; } = null;
        public virtual DbSet<SmartZoneEmployee> SmartZoneEmployees { get; set; } = null;
        public virtual DbSet<SmartZoneCustomer> SmartZoneCustomers { get; set; } = null;
        public virtual DbSet<Store> Stores { get; set; } = null;
        public virtual DbSet<Food> Foods { get; set; } = null;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<SmartZone>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            builder.Entity<Store>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.HasOne(s => s.SmartZone)
                    .WithMany(smz => smz!.Stores)
                    .HasForeignKey(s => s!.SmartZoneId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<SmartZoneEmployee>(entity =>
            {
                entity.HasOne(s => s.Store)
                    .WithMany(e => e!.Employees)
                    .HasForeignKey(s => s!.StoreId)
                    .OnDelete(DeleteBehavior.SetNull);
            });


            builder.Entity<Food>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.HasOne(s => s.Store)
                    .WithMany(f => f!.Foods)
                    .HasForeignKey(s => s!.StoreId)
                    .OnDelete(DeleteBehavior.SetNull);
            });
        }
    }
}
