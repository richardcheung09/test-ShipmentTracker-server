using Microsoft.EntityFrameworkCore;
using ShipmentTracker.Infrastructure;

namespace ShipmentTracker.Test;

public static class TestDbContextFactory
{
    public static ShipmentTrackerDbContext CreateTestDbContext()
    {
        var options = new DbContextOptionsBuilder<ShipmentTrackerDbContext>()
            .UseSqlite("Data Source=TestDB.db")
            .Options;

        var context = new ShipmentTrackerDbContext(options);
        context.Database.EnsureDeleted(); // Ensure a clean database for each test run
        context.Database.EnsureCreated();

        return context;
    }
}