using Application.Dtos;
using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Orders.Commands.CreateOrder;

public record class CreateOrderCommand : IRequest<Guid>
{
    public Guid UserId { get; init;}
    public decimal TotalAmount { get; set; }
    public required List<OrderItemDto> OrderItems { get; set; }
    public required ShippingAddressDto ShippingAddress { get; set; }
}

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Guid>
{
    private readonly IApplicationDbContext dbContext;

    public CreateOrderCommandHandler(IApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public  async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var newOrder = new Order
            {
                UserId = request.UserId,
                OrderDate = DateTime.UtcNow,
                TotalAmount = request.TotalAmount,
                Status = OrderStatus.Pending,
                OrderItems = request.OrderItems.Select(oi => new OrderItem
                {
                    GameId = oi.GameId,
                    Quantity = oi.Quantity,
                    Price = oi.Price
                }).ToList(),
                ShippingAddress = new ShippingAddress
                {
                    AddressLine = request.ShippingAddress.AddressLine,
                    City = request.ShippingAddress.City,
                    State = request.ShippingAddress.State,
                    ZipCode = request.ShippingAddress.ZipCode,
                    Country = request.ShippingAddress.Country
                }
            };

            dbContext.Orders.Add(newOrder);

            await dbContext.SaveChangesAsync(cancellationToken);

            return newOrder.Id;
        }
}