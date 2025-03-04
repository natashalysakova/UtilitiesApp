using Infrastructure.Models;

namespace ApiService.DTO;

public record TariffViewDto
{
    public Guid Id { get; set; }

    public Guid UtilityGroupId { get; set; }

    public required string UtilityGroupName { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public decimal Cost { get; set; }

    public TariffType TariffType { get; set; }

    public string? Units { get; set; }

    public bool UseLimits { get; set; }

    public ICollection<LimitViewDto> Limits { get; set; } = [];

    public bool UseAdditionalFee { get; set; }

    public string? AdditionalFeeName { get; set; }

    public decimal? AdditionalFeeCost { get; set; }

    public bool UseFixedPay { get; set; }

    public string? FixedPayName { get; set; }

    public decimal? FixedPay { get; set; }

    public required HomeSmallView Home { get; set; }
}