namespace ApiService.DTO;

public record RecordViewDto
{
    public Guid Id { get; set; }

    public decimal Measure { get; set; }

    public decimal PreviousMeasure { get; set; }

    public decimal Difference { get; set; }

    public decimal Cost { get; set; }

    public decimal AdditionalSum { get; set; }

    public Guid TariffId { get; set; }

    public required TariffViewDto Tariff { get; set; }
}
