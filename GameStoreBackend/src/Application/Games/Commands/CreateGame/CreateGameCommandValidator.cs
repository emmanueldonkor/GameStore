using FluentValidation;

namespace Application.Games.Commands.CreateGame;

public class CreateGameCommandValidator : AbstractValidator<CreateGameCommand>
{
    public CreateGameCommandValidator()
    {
        RuleFor(g => g.Name).NotEmpty().WithMessage("Name is Required");
        RuleFor(g => g.Description).NotEmpty().WithMessage("Description is Required");
        RuleFor(g => g.Category).NotEmpty().WithMessage("Category is Required");
        RuleFor(g => g.Price).GreaterThan(0).WithMessage("Price should be greater than 0");
        RuleFor(g => g.ImageUrl).NotEmpty().WithMessage("ImageUrl is Required");
    }
}
