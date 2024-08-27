using Application.Checkout;
using AutoMapper;
using Domain.Entities;

namespace Application.Common.Mappings;

public class CheckoutProfile : Profile
{
    public CheckoutProfile()
    {
        CreateMap<CreateCheckoutCommand, Order>()
        .ForMember(dest => dest.OrderDate, opt => opt.MapFrom(src => DateTime.UtcNow))
        .ForMember(dest => dest.Status, opt => opt.MapFrom(src => OrderStatus.Pending))
        .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems))
        .ForMember(dest => dest.ShippingAddress, opt => opt.MapFrom(src => src.ShippingAddress));
    }
}