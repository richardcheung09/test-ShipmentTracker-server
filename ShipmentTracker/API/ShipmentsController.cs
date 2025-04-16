using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShipmentTracker.Domain;
using ShipmentTracker.Infrastructure;

namespace ShipmentTracker.API;

[ApiController]
[Route("api/[controller]")]
public class ShipmentsController : ControllerBase
{
    private readonly ShipmentTrackerDbContext _context;

    public ShipmentsController(ShipmentTrackerDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetShipments([FromQuery] ShipmentStatus? status, [FromQuery] string? carrier)
    {
        var query = _context.Shipments.AsQueryable();

        if (status.HasValue)
        {
            query = query.Where(s => s.Status == status);
        }

        if (!string.IsNullOrEmpty(carrier))
        {
            query = query.Where(s => s.Carrier == carrier);
        }

        var shipments = await query.ToListAsync();
        return Ok(shipments);
    }

    [HttpPost]
    public async Task<IActionResult> AddShipment([FromBody] Shipment shipment)
    {
         using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            _context.Shipments.Add(shipment);
            await _context.SaveChangesAsync();

            var carrier = await _context.Carriers.FirstOrDefaultAsync(c => c.Name == shipment.Carrier);
            if (carrier == null)
            {
                carrier = new Carrier { Name = shipment.Carrier };
                _context.Carriers.Add(carrier);
                await _context.SaveChangesAsync();
            }

            await transaction.CommitAsync();
            return CreatedAtAction(nameof(GetShipments), new { id = shipment.Id }, shipment);
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    [HttpPut("{id}/status")]
    public async Task<IActionResult> UpdateShipmentStatus(int id, [FromBody] int status)
    {
        var shipment = await _context.Shipments.FindAsync(id);
        if (shipment == null)
        {
            return NotFound();
        }

        shipment.Status = (ShipmentStatus)status;
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
