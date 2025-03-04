namespace ApiService.DTO;

public record LimitEditDto
{
    public Guid Id { get; set; }

    public decimal Limit { get; set; }

    public decimal CostAfterLimit { get; set; }

    public Guid TariffId { get; set; }
}
