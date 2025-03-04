 using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Extensions;

internal static class TariffCollectionExtension
{
    internal static async Task<IEnumerable<Tariff>> CurrentlyActive(this IQueryable<Tariff> tariffs, Guid houseId)
    {
        return await tariffs.ActiveOnDate(houseId, DateTime.Now);
    }

    internal static async Task<IEnumerable<Tariff>> CurrentlyNotActive(this IQueryable<Tariff> tariffs, Guid houseId)
    {
        return await tariffs.NotActiveOnDate(houseId, DateTime.Now);
    }

    internal static async Task<IEnumerable<Tariff>> ActiveOnDate(this IQueryable<Tariff> tariffs, Guid houseId, DateTime date)
    {
        var houseTarifs = await tariffs.Where(x => x.HomeId == houseId).ToListAsync();
        return houseTarifs.Where(x => date.IsBetween(x.StartDate, x.EndDate));
    }

    internal static async Task<IEnumerable<Tariff>> NotActiveOnDate(this IQueryable<Tariff> tariffs, Guid houseId, DateTime date)
    {
        var houseTarifs = await tariffs.Where(x => x.HomeId == houseId).ToListAsync();
        return houseTarifs.Where(x => !date.IsBetween(x.StartDate, x.EndDate));
    }

    internal static bool IsBetween(this DateTime date, DateTime begin, DateTime? end)
    {
        if (end is null)
        {
            return date >= begin;
        }

        return date >= begin && date <= end;
    }
}
