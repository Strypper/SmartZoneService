using Microsoft.EntityFrameworkCore;

namespace SmartZone.Entities
{
    public class SmartZoneContext : DbContext
    {
        public SmartZoneContext(DbContextOptions<SmartZoneContext> options) : base(options) { }

        public virtual DbSet<SmartZone> SmartZones { get; set; } = null;
        public virtual DbSet<SmartZoneEmployee> SmartZoneEmployees { get; set; } = null;
        public virtual DbSet<SmartZoneCustomer> SmartZoneCustomers { get; set; } = null;
        public virtual DbSet<Store> AddressInfos { get; set; } = null;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<SmartZone>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            builder.Entity<SmartZoneEmployee>(entity =>
            {
                entity.HasOne(s => s.Store)
                    .WithMany(e => e!.Employees)
                    .HasForeignKey(s => s!.StroreId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
