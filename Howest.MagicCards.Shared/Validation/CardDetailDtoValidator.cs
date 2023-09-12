
using FluentValidation;
using Howest.MagicCards.Shared.Dtos;

namespace Howest.MagicCards.Shared.Validation;

public class CardDetailDtoValidator : AbstractValidator<CardDetailDto>
{
    public CardDetailDtoValidator()
    {
        RuleFor(x => x.Id)
            .Cascade(CascadeMode.Stop)
            .NotNull().WithMessage("Id is required.")
            .GreaterThan(0).WithMessage("Id must be greater than 0.");

        RuleForEach(x => new List<string> { x.Name, x.SetName, x.ArtistName, x.RarityName, x.ManaCost, x.ConvertedManaCost, x.Layout })
            .Cascade(CascadeMode.Stop)
            .NotNull().WithMessage("{PropertyName} is required.")
            .NotEmpty().WithMessage("{PropertyName} must not be empty.");
    }
}
