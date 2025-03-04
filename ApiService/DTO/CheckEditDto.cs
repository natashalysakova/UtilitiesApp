using System.ComponentModel.DataAnnotations;

namespace ApiService.DTO;

public record CheckEditDto
{
    public Guid Id { get; set; }

    [DataType(DataType.Date)]
    public DateTime? Date { get; set; }

    [DataType(DataType.Currency)]
    public decimal Amount { get; set; }

    [Required]
    public Guid HomeId { get; set; }

    public double HomeArea { get; internal set; }

    public ICollection<RecordEditDto> Records { get; set; } = [];

    public bool IsZeroCheck { get; set; }
}
