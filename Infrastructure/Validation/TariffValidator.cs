using FluentValidation;
using Infrastructure.Models;

namespace Infrastructure.Validation;

public class TariffValidator : AbstractValidator<Tariff>
{
    public TariffValidator()
    {
        RuleFor(x => x.HomeId).NotEmpty();
        RuleFor(x => x.UtilityGroupId).NotEmpty();

        RuleFor(x => x.StartDate).NotEmpty();

        When(x => x.EndDate != null, () =>
        {
            RuleFor(x => x.EndDate).NotEmpty().GreaterThan(x => x.StartDate);
        });

        RuleFor(x => x.Cost).GreaterThan(0);

        RuleFor(x => x.TariffType).IsInEnum();

        When(x => x.TariffType == TariffType.Meters, () =>
        {
            RuleFor(x => x.Units).NotEmpty();
        });

        When(x => x.UseLimits, () =>
        {
            RuleFor(x => x.Limits).NotEmpty();
        });

        When(x => x.UseAdditionalFee, () =>
        {
            RuleFor(x => x.AdditionalFeeName).NotNull();
            RuleFor(x => x.AdditionalFeeCost).NotNull().GreaterThan(0);
        });

        When(x => x.UseFixedPay, () =>
        {
            RuleFor(x => x.FixedPayName).NotNull();
            RuleFor(x => x.FixedPay).NotNull().GreaterThan(0);
        });

        RuleFor(x => x.IsArchived).NotEqual(true);
    }
}
