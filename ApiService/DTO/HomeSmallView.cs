namespace ApiService.DTO;

public record HomeSmallView
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public double Area { get; set; }
}
