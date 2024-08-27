using Domain.Entities;

namespace Application.Common.Dtos;

public record class PaymentDto
{
    public Guid OrderId { get; init;}
    public required string PaymentMethod { get; init; }
    public required string TransactionId { get; init; }
    public decimal Amount { get; init; }
    public DateTime PaymentDate { get; init; }
    public PaymentStatus PaymentStatus { get; init; }
}