using FluentValidation;

namespace Application.Games.Commands.UpdateGame;

public class UpdateGameCommandValidator : AbstractValidator<UpdateGameCommand>
{
    public UpdateGameCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required.");
        RuleFor(x => x.Genre).NotEmpty().WithMessage("Genre is required.");
        RuleFor(x => x.Price).GreaterThan(0).When(x => x.Price.HasValue).WithMessage("Price must be a positive value.");
        RuleFor(x => x.ImageUrl).NotEmpty().WithMessage("ImageUrl is required.");
    }
}
