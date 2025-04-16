using Microsoft.AspNetCore.Mvc;
using ShipmentTracker.API;
using ShipmentTracker.Domain;

namespace ShipmentTracker.Test.Controllers;

public class ShipmentsControllerTests
{
    [Fact]
    public async Task GetShipments_ShouldReturnFilteredShipments()
    {
        // Arrange
        var context = TestDbContextFactory.CreateTestDbContext();
        context.Shipments.Add(new Shipment { Id = 1, Carrier = "FedEx", Status = ShipmentStatus.Pending });
        context.Shipments.Add(new Shipment { Id = 2, Carrier = "UPS", Status = ShipmentStatus.Delivered });
        context.SaveChanges();

        var controller = new ShipmentsController(context);

        // Act
        var result = await controller.GetShipments(ShipmentStatus.Pending, null);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var shipments = Assert.IsType<List<Shipment>>(okResult.Value);
        Assert.Single(shipments);
        Assert.Equal("FedEx", shipments[0].Carrier);
    }

    [Fact]
    public async Task AddShipment_ShouldAddShipmentToDatabase()
    {
        // Arrange
        var context = TestDbContextFactory.CreateTestDbContext();
        var controller = new ShipmentsController(context);
        var newShipment = new Shipment
        {
            Id = 3,
            Origin = "New York",
            Destination = "Los Angeles",
            Carrier = "FedEx",
            ShipDate = DateTime.Now,
            ETA = DateTime.Now.AddDays(5),
            Status = ShipmentStatus.Pending
        };

        // Act
        var result = await controller.AddShipment(newShipment);

        // Assert
        var okResult = Assert.IsType<CreatedAtActionResult>(result);
        var addedShipment = context.Shipments.FirstOrDefault(s => s.Id == newShipment.Id);
        Assert.NotNull(addedShipment);
        Assert.Equal("New York", addedShipment.Origin);
    }
}
