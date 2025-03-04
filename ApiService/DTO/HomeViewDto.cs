namespace ApiService.DTO;

public record HomeViewDto
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Country { get; set; } = string.Empty;

    public string City { get; set; } = string.Empty;

    public string Region { get; set; } = string.Empty;

    public string Street { get; set; } = string.Empty;

    public string Building { get; set; } = string.Empty;

    public string? Apartment { get; set; }

    public required double Area { get; set; }

    public ICollection<TariffViewDto> Tariffs { get; set; } = [];

    public ICollection<CheckViewDto> Checks { get; set; } = [];

    public bool IsDefault { get; set; }
}
