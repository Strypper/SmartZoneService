using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SmartZone.Entities
{
    public class SmartZoneContext : IdentityDbContext<User, 
                                                        Role, 
                                                        string, 
                                                        IdentityUserClaim<string>, 
                                                        UserRole, 
                                                        IdentityUserLogin<string>, 
                                                        IdentityRoleClaim<string>, 
                                                        IdentityUserToken<string>>
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

            // TPT
            builder.Entity<Customer>().ToTable("Customers");
            builder.Entity<Employee>().ToTable("Employees");


            builder.Entity<User>(entity =>
            {
                entity.Property(usr => usr.Guid).HasDefaultValueSql("NEWID()");
                entity.HasIndex(usr => usr.Guid).IsUnique();
                entity.HasIndex(usr => usr.UserName).IsUnique();
            });


            builder.Entity<UserRole>(entity =>
            {
                entity.HasOne(u_r => u_r.Role).WithMany(rol => rol!.UserRoles).HasForeignKey(u_r => u_r.RoleId).IsRequired().OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(u_r => u_r.User).WithMany(usr => usr!.UserRoles).HasForeignKey(u_r => u_r.UserId).IsRequired().OnDelete(DeleteBehavior.Cascade);
            });


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
                entity.HasOne(emp => emp.Store)
                    .WithMany(sto => sto.Employees)
                    .HasForeignKey(emp => emp.StoreId)
                    .OnDelete(DeleteBehavior.Restrict);
            });


            builder.Entity<Customer>(entity =>
            {
                
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
