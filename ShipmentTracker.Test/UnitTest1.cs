using Moq;
using ShipmentTracker.Domain;
using ShipmentTracker.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace ShipmentTracker.Test;

public class UnitTest1
{
    [Fact]
    public void AddShipment_ShouldAddShipmentToDatabase()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ShipmentTrackerDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        using var context = new ShipmentTrackerDbContext(options);
        var shipment = new Shipment
        {
            Id = 1,
            Origin = "New York",
            Destination = "Los Angeles",
            Carrier = "FedEx",
            ShipDate = DateTime.Now,
            ETA = DateTime.Now.AddDays(5),
            Status = ShipmentStatus.Pending
        };

        // Act
        context.Shipments.Add(shipment);
        context.SaveChanges();

        // Assert
        var addedShipment = context.Shipments.FirstOrDefault(s => s.Id == shipment.Id);
        Assert.NotNull(addedShipment);
        Assert.Equal("New York", addedShipment.Origin);
        Assert.Equal("Los Angeles", addedShipment.Destination);
        Assert.Equal("FedEx", addedShipment.Carrier);
    }
}