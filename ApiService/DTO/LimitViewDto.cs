namespace ApiService.DTO;

public record LimitViewDto
{
    public Guid Id { get; set; }

    public decimal Limit { get; set; }

    public decimal CostAfterLimit { get; set; }
}
