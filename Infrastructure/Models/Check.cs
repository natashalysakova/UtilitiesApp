using System.ComponentModel.DataAnnotations.Schema;
using Infrastructure.Interfaces;

namespace Infrastructure.Models;

public class Check : IArchivableEntity, IAuditable
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    public required DateTime Date { get; set; }

    public decimal Amount { get; set; }

    public Guid HomeId { get; set; }

    public virtual Home? Home { get; set; }

    public virtual ICollection<Record> Records { get; set; } = [];

    public bool IsZeroCheck { get; set; }

    public bool IsArchived { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public DateTime? DeletedAt { get; set; }
}