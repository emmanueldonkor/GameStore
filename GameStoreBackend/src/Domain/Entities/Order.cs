namespace Domain.Entities;

public class Order
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public OrderStatus Status { get; set; }
     public IReadOnlyList<OrderItem>? OrderItems { get; set; }
    public Payment? Payment { get; set; }
     public ShippingAddress? ShippingAddress { get; set; } 
}

public enum OrderStatus
{
    Pending,
    Paid,
    Shipped,
    Delivered,
    Cancelled,
    Completed
}
