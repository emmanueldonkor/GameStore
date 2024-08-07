namespace Domain.Entities;

public class ShippingAddress
{
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public required string AddressLine { get; set; }
    public required string City { get; set; }
    public required string State { get; set; }
    public required string ZipCode { get; set; }
    public required string Country { get; set; }
}
