using System.ComponentModel.DataAnnotations.Schema;
using Infrastructure.Interfaces;

namespace Infrastructure.Models;
public class Tariff : IArchivableEntity, IAuditable
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    public Guid UtilityGroupId { get; set; }

    public UtilityGroup? UtilityGroup { get; set; }

    public required DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public int Order { get; set; }

    public required decimal Cost { get; set; }

    public required TariffType TariffType { get; set; }

    public string? Units { get; set; }

    public bool UseLimits { get; set; }

    public virtual ICollection<TariffLimit> Limits { get; set; } = [];

    public bool UseAdditionalFee { get; set; }

    public string? AdditionalFeeName { get; set; }

    public decimal? AdditionalFeeCost { get; set; }

    public bool UseFixedPay { get; set; }

    public string? FixedPayName { get; set; }

    public decimal? FixedPay { get; set; }

    public Guid HomeId { get; set; }

    public virtual Home? Home { get; set; }

    public bool IsArchived { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public DateTime? DeletedAt { get; set; }
}