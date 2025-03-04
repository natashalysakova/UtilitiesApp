using FluentValidation;
using Infrastructure.Models;

namespace Infrastructure.Validation;

public class HomeValidator : AbstractValidator<Home>
{
    public HomeValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Country).NotEmpty();
        RuleFor(x => x.City).NotEmpty();
        RuleFor(x => x.Region).NotEmpty();
        RuleFor(x => x.Street).NotEmpty();
        RuleFor(x => x.Building).NotEmpty();
        RuleFor(x => x.Area).GreaterThan(0);
        RuleFor(x => x.Apartment).NotEmpty().Unless(x => x.Apartment == null);
        RuleFor(x => x.IsArchived).NotEqual(true);
    }
}
