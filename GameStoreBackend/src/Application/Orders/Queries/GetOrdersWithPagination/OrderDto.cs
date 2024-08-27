using Application.Common.Dtos;
using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;

namespace Application.Orders.Queries.GetOrdersWithPagination;

public record class OrderDto : IMapFrom<Order>
{
    public Guid Id { get; init; }
    public required List<OrderItemDto> OrderItems { get; init; }
    public required ShippingAddressDto ShippingAddress { get; init; }
    public required PaymentDto Payment { get; init; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Order, OrderDto>()
            .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems))
            .ForMember(dest => dest.ShippingAddress, opt => opt.MapFrom(src => src.ShippingAddress))
            .ForMember(dest => dest.Payment, opt => opt.MapFrom(src => src.Payment));
    }
}