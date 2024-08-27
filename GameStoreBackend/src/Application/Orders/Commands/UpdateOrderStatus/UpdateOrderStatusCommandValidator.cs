using FluentValidation;

namespace Application.Orders.Commands.UpdateOrderStatus;

public class UpdateOrderStatusValidator : AbstractValidator<UpdateOrderStatusCommand>
{
    public UpdateOrderStatusValidator()
    {
        RuleFor(x => x.OrderId).NotEmpty().WithMessage("OrderId is required");
        RuleFor(x => x.Status).NotEmpty().WithMessage("Status is required");
    }
}