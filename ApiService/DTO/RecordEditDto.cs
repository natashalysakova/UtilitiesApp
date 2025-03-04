using System.ComponentModel.DataAnnotations;

namespace ApiService.DTO;

public record RecordEditDto
{
    public Guid Id { get; set; }

    [Required]
    public decimal Measure { get; set; }

    public decimal PreviousMeasure { get; set; }

    [Required]
    public decimal Difference { get; set; }

    [DataType(DataType.Currency)]
    public decimal Cost { get; set; }

    [DataType(DataType.Currency)]
    public decimal AdditionalSum { get; set; }

    public Guid TariffId { get; set; }

    public Guid CheckId { get; set; }

    public TariffCalulateDto Tariff { get; set; } = new TariffCalulateDto();
}
