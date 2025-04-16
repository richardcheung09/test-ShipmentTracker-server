using Microsoft.AspNetCore.Mvc;
using ShipmentTracker.API;
using ShipmentTracker.Domain;

namespace ShipmentTracker.Test.Controllers;

public class CarriersControllerTests
{
    [Fact]
    public async Task GetCarriers_ShouldReturnListOfCarriers()
    {
        // Arrange
        var context = TestDbContextFactory.CreateTestDbContext();
        context.Carriers.Add(new Carrier { Id = 1, Name = "Carrier1" });
        context.Carriers.Add(new Carrier { Id = 2, Name = "Carrier2" });
        context.SaveChanges();

        var controller = new CarriersController(context);

        // Act
        var result = await controller.GetCarriers();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var carriers = Assert.IsType<List<Carrier>>(okResult.Value);
        Assert.Equal(2, carriers.Count);
    }
}
