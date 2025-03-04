using System.ComponentModel.DataAnnotations.Schema;
using Infrastructure.Interfaces;

namespace Infrastructure.Models;

public class Home : IArchivableEntity, IAuditable, IDefaultable
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    public required string Name { get; set; }

    public required string Country { get; set; }

    public required string City { get; set; }

    public required string Region { get; set; }

    public required string Street { get; set; }

    public required string Building { get; set; }

    public string? Apartment { get; set; }

    public required double Area { get; set; }

    public virtual ICollection<Tariff> Tariffs { get; set; } = [];

    public virtual ICollection<Check> Checks { get; set; } = [];

    public bool IsDefault { get; set; }

    public Guid Scope { get; set; }

    public bool IsArchived { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public DateTime? DeletedAt { get; set; }
}