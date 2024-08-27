namespace Application.Common.Dtos;

public record class ShippingAddressDto
{
    public required string AddressLine { get; init; }
    public required string City { get; init; }
    public required string State { get; init; }
    public required string ZipCode { get; init; }
    public required string Country { get; init; }
}