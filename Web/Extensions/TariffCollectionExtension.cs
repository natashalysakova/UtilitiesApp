using Web.Clients;

namespace Web.Extensions;

public static class TariffCollectionExtension
{
    public static ICollection<TariffViewDto> CurrentlyActive(this ICollection<TariffViewDto> tariffs)
    {
        return tariffs.ActiveOnDate(DateTime.Now);
    }

    public static ICollection<TariffViewDto> CurrentlyNotActive(this ICollection<TariffViewDto> tariffs)
    {
        return tariffs.NotActiveOnDate(DateTime.Now);
    }

    public static ICollection<TariffViewDto> ActiveOnDate(this ICollection<TariffViewDto> tariffs, DateTime date)
    {
        return tariffs.Where(x => date.IsBetween(x.StartDate, x.EndDate)).ToList();
    }

    public static ICollection<TariffViewDto> NotActiveOnDate(this ICollection<TariffViewDto> tariffs, DateTime date)
    {
        return tariffs.Where(x => !date.IsBetween(x.StartDate, x.EndDate)).ToList();
    }

    public static bool IsBetween(this DateTime date, DateTime begin, DateTime? end)
    {
        if (end is null)
        {
            return date >= begin;
        }

        return date >= begin && date <= end;
    }
}
