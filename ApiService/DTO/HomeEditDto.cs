using System.ComponentModel.DataAnnotations;

namespace ApiService.DTO;

public record HomeEditDto
{
    public Guid Id { get; set; }

    [Required]
    [MaxLength(32)]
    public required string Name { get; set; }

    [Required]
    [MaxLength(96)]
    public required string Country { get; set; }

    [Required]
    [MaxLength(96)]
    public required string City { get; set; }

    [Required]
    [MaxLength(96)]
    public required string Region { get; set; }

    [Required]
    [MaxLength(96)]
    public required string Street { get; set; }

    [Required]
    [MaxLength(96)]
    public required string Building { get; set; }

    [MaxLength(32)]
    public string? Apartment { get; set; }

    public required double Area { get; set; }

    public bool IsDefault { get; set; }
}
