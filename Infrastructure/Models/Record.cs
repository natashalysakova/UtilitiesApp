using System.ComponentModel.DataAnnotations.Schema;
using Infrastructure.Interfaces;

namespace Infrastructure.Models;

public class Record : IArchivableEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    public decimal Measure { get; set; }

    public decimal PreviousMeasure { get; set; }

    public decimal Difference { get; set; }

    public decimal Cost { get; set; }

    public decimal AdditionalSum { get; set; }

    public Guid CheckId { get; set; }

    public virtual Check? Check { get; set; }

    public Guid TariffId { get; set; }

    public virtual Tariff? Tariff { get; set; }

    public bool IsArchived { get; set; }
}