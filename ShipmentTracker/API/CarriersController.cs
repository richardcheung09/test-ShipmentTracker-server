using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShipmentTracker.Infrastructure;

namespace ShipmentTracker.API;

[ApiController]
[Route("api/[controller]")]
public class CarriersController : ControllerBase
{
    private readonly ShipmentTrackerDbContext _context;

    public CarriersController(ShipmentTrackerDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetCarriers()
    {
        var carriers = await _context.Carriers.ToListAsync();
        return Ok(carriers);
    }
}
