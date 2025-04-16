using System.ComponentModel.DataAnnotations;

namespace ShipmentTracker.Domain;

public class Shipment
{
    public int Id { get; set; }
    public string Origin { get; set; } = string.Empty;
    public string Destination { get; set; } = string.Empty;
    public string Carrier { get; set; } = string.Empty;
    public DateTime ShipDate { get; set; }
    public DateTime ETA { get; set; }
    public ShipmentStatus Status { get; set; } = ShipmentStatus.Pending;
}

public enum ShipmentStatus
{
    [Display(Description = "[0] The shipment is pending and has not been shipped yet.")]
    Pending,

    [Display(Description = "[1] The shipment has been shipped and is in transit.")]
    Shipped,

    [Display(Description = "[2] The shipment has been delivered to the destination.")]
    Delivered,

    [Display(Description = "[3] The shipment has been cancelled.")]
    Cancelled
}
