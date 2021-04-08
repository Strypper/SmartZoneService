using Microsoft.EntityFrameworkCore;

namespace SmartZone.Entities
{
    public class SmartZoneContext : DbContext
    {
        public SmartZoneContext(DbContextOptions<SmartZoneContext> options) : base(options) { }

        public virtual DbSet<SmartZone> SmartZones { get; set; } = null;
        public virtual DbSet<SmartZoneUser> SmartZoneUsers { get; set; } = null;
        public virtual DbSet<AddressInfo> AddressInfos { get; set; } = null;

    }
}
