using Microsoft.EntityFrameworkCore;

namespace SmartZone.Entities
{
    public class SmartZoneContext : DbContext
    {
        public SmartZoneContext(DbContextOptions<SmartZoneContext> options) : base(options) { }

        public virtual DbSet<SmartZone> SmartZones { get; set; } = null;
        public virtual DbSet<Employee> SmartZoneEmployees { get; set; } = null;
        public virtual DbSet<Customer> SmartZoneCustomers { get; set; } = null;
        public virtual DbSet<Store> Stores { get; set; } = null;
        public virtual DbSet<Food> Foods { get; set; } = null;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<SmartZone>(entity =>
            {
                entity.Property(smz => smz.Guid).HasDefaultValueSql("NEWID()");
                entity.HasIndex(smz => smz.Guid).IsUnique();
            });


            builder.Entity<Store>(entity =>
            {
                entity.HasOne(sto => sto.SmartZone)
                    .WithMany(smz => smz.Stores)
                    .HasForeignKey(sto => sto.SmartZoneId)
                    .OnDelete(DeleteBehavior.Cascade);
            });


            builder.Entity<Employee>(entity =>
            {
                entity.Property(cus => cus.Guid).HasDefaultValueSql("NEWID()");
                entity.HasIndex(cus => cus.Guid).IsUnique();

                entity.HasOne(emp => emp.Store)
                    .WithMany(sto => sto.Employees)
                    .HasForeignKey(emp => emp.StoreId)
                    .OnDelete(DeleteBehavior.Restrict);
            });


            builder.Entity<Customer>(entity =>
            {
                entity.Property(cus => cus.Guid).HasDefaultValueSql("NEWID()");
                entity.HasIndex(cus => cus.Guid).IsUnique();
            });


            builder.Entity<Order>(entity =>
            {
                entity.HasOne(ord => ord.Store)
                    .WithMany(sto => sto!.Orders)
                    .HasForeignKey(ord => ord!.StoreId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(ord => ord.Customer)
                    .WithMany(cus => cus!.Orders)
                    .HasForeignKey(ord => ord!.CustomerId)
                    .OnDelete(DeleteBehavior.Restrict);
            });


            builder.Entity<Food>(entity =>
            {
                entity.HasOne(foo => foo.Store)
                    .WithMany(sto => sto!.Foods)
                    .HasForeignKey(foo => foo!.StoreId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.Property(p => p.Price)
                    .HasColumnType("decimal(18,4)");
            });
        }
    }
}
