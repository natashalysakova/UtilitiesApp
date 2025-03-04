namespace Infrastructure.Models;

public enum TariffType
{
    /// <summary>
    /// Fixed payment.
    /// </summary>
    Fixed = 0,

    /// <summary>
    /// Payment depends on the area of the house.
    /// </summary>
    HouseHoldArea = 1,

    /// <summary>
    /// Payment depends on the meters.
    /// </summary>
    Meters = 2,

    /// <summary>
    /// Payment is not fixed and has no dependency.
    /// </summary>
    NotFixedPayment = 3,

    /// <summary>
    /// Payment depends on consumed volume but without meter.
    /// </summary>
    Volume = 4,
}