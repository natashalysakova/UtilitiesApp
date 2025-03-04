using Infrastructure.Models;

namespace ApiService.DTO;

public record TariffCalulateDto
{
    public Guid Id { get; set; }

    public string UtilityGroupName { get; set; } = string.Empty;

    public Guid UtilityGroupId { get; set; }

    public string? Units { get; set; }

    public decimal Cost { get; set; }

    public TariffType TariffType { get; set; }

    public string? FixedPayName { get; set; }

    public decimal FixedPay { get; set; }

    public bool UseFixedPay { get; set; }

    public double HomeArea { get; set; }

    public string? AdditionalFeeName { get; set; }

    public decimal AdditionalFeeCost { get; set; }

    public bool UseAdditionalFee { get; set; }

    public DateTime StartDate { get; set; }
}
