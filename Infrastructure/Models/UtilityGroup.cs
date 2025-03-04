using System.ComponentModel.DataAnnotations.Schema;
using Infrastructure.Interfaces;

namespace Infrastructure.Models;

public class UtilityGroup : IArchivableEntity, IAuditable
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    public required string Name { get; set; }

    public int Priority { get; set; }

    public bool IsArchived { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public DateTime? DeletedAt { get; set; }
}
