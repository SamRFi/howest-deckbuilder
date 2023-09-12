
using FluentValidation;
using Howest.MagicCards.Shared.Dtos;

namespace Howest.MagicCards.Shared.Validation;

public class CardListDtoValidator : AbstractValidator<CardListDto>
{
    public CardListDtoValidator()
    {
        RuleFor(x => x.Id)
            .Cascade(CascadeMode.Stop)
            .NotNull().WithMessage("Id is required.")
            .GreaterThan(0).WithMessage("Id must be greater than 0.");

        RuleForEach(x => new List<string> { x.Name, x.SetName, x.ArtistName, x.RarityName })
            .Cascade(CascadeMode.Stop)
            .NotNull().WithMessage("{PropertyName} is required.")
            .NotEmpty().WithMessage("{PropertyName} must not be empty.");
    }
}
