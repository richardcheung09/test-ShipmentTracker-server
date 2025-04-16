using Microsoft.EntityFrameworkCore;
using ShipmentTracker.Domain;

namespace ShipmentTracker.Infrastructure
{
    public class ShipmentTrackerDbContext : DbContext
    {
        public ShipmentTrackerDbContext(DbContextOptions<ShipmentTrackerDbContext> options) : base(options)
        {
        }

        public DbSet<Shipment> Shipments { get; set; }
        public DbSet<Carrier> Carriers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
