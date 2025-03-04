using Infrastructure.Interfaces;

namespace Infrastructure.Models;

public class TariffLimit : IArchivableEntity
{
    public Guid Id { get; set; }

    public Guid TariffId { get; set; }

    public virtual Tariff? Tariff { get; set; }

    public decimal Limit { get; set; }

    public decimal CostAfterLimit { get; set; }

    public bool IsArchived { get; set; }
}