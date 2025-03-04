using System.ComponentModel.DataAnnotations;

namespace ApiService.DTO;

public record CheckViewDto
{
    public Guid Id { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime Date { get; set; }

    [DataType(DataType.Currency)]
    public decimal Amount { get; set; }

    public HomeSmallView Home { get; set; } = new HomeSmallView();

    public ICollection<RecordViewDto> Records { get; set; } = [];

    public bool IsZeroCheck { get; set; }
}
