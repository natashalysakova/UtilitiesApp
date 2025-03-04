using Infrastructure.Extensions;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class CheckCalculationService(UtilitiesDbContext context)
{
    public async Task<decimal> Calculate(Check check)
    {
        if (check.IsZeroCheck)
        {
            check.Amount = 0;
            return check.Amount;
        }

        double? homearea = null;

        if (check.Records.Any(x => x.Tariff!.TariffType == TariffType.HouseHoldArea))
        {
            var home = await context.Homes.FindAsync(check.HomeId);
            homearea = home?.Area;
        }

        foreach (var item in check.Records)
        {
            await Calculate(item, check.IsZeroCheck, homearea);
        }

        check.Amount =
        check.Records.Sum(x => x.Cost) +
        check.Records.Sum(x => x.AdditionalSum) +
        check.Records.Where(x => x.Tariff!.UseFixedPay).Sum(x => x.Tariff!.FixedPay!.Value);

        return check.Amount;
    }

    public async Task Calculate(Record record, bool isZero, double? homeArea = null)
    {
        if (isZero)
        {
            record.Cost = 0;
            record.AdditionalSum = 0;
            return;
        }

        if (record.Tariff!.TariffType == TariffType.HouseHoldArea && homeArea is null)
        {
            var homeId = context.Tariffs.Find(record.TariffId)!.HomeId;
            var home = await context.Homes.FindAsync(homeId);
            homeArea = home!.Area;
        }

        switch (record.Tariff!.TariffType)
        {
            case TariffType.Fixed:
                record.Cost = record.Tariff.Cost;
                record.PreviousMeasure = 0;
                record.Difference = 0;
                break;
            case TariffType.HouseHoldArea:
                record.Cost = record.Tariff.Cost * (decimal)homeArea!;
                record.PreviousMeasure = 0;
                record.Difference = 0;
                break;
            case TariffType.Meters:
                if (record.Measure != 0)
                {
                    record.Difference = record.Measure - record.PreviousMeasure;
                    record.Cost = CalculateMeters(record, record.Tariff);
                }
                else
                {
                    record.Difference = 0;
                    record.Cost = 0;
                }

                break;
            case TariffType.Volume:
                record.Cost = record.Measure * record.Tariff.Cost;
                record.PreviousMeasure = 0;
                record.Difference = 0;
                break;
            case TariffType.NotFixedPayment: // do nothing, cost will adds up later
                record.PreviousMeasure = 0;
                record.Difference = 0;
                record.Measure = 0;
                break;
            default:
                break;
        }

        if (record.Tariff.UseAdditionalFee)
        {
            record.AdditionalSum = record.Difference * record.Tariff.AdditionalFeeCost!.Value;
        }
    }

    public async Task<Check> CreateEmpty(Guid houseId, DateTime date)
    {
        var check = new Check() { HomeId = houseId, Date = date };

        return await Refill(check);
    }

    public async Task<Check> Refill(Check check)
    {
        var tariffs = await context.Tariffs.Include(x => x.Home).Include(x => x.UtilityGroup).Include(x => x.Limits).ActiveOnDate(check.HomeId, check.Date);

        foreach (var tariff in tariffs)
        {
            decimal previousMeasure = await GetPreviousMeasure(tariff.UtilityGroupId, check.HomeId);

            var record = check.Records.SingleOrDefault(x => x.TariffId == tariff.Id);
            if (record == null)
            {
                check.Records.Add(new Record() { TariffId = tariff.Id, Tariff = tariff, PreviousMeasure = previousMeasure, CheckId = check.Id });
            }
            else
            {
                record.PreviousMeasure = previousMeasure;
            }
        }

        var toDelete = check.Records.Where(x => !tariffs.Select(y => y.Id).Contains(x.TariffId)).ToList();
        foreach (var record in toDelete)
        {
            check.Records.Remove(record);
        }

        await Calculate(check);

        return check;
    }

    public async Task<Check?> GetPreviousCheck(Guid homeId, DateTime date)
    {
        return await context.Checks
            .Include(x => x.Records).ThenInclude(x => x.Tariff).ThenInclude(x => x!.Limits)
            .Include(x => x.Records).ThenInclude(x => x.Tariff).ThenInclude(x => x!.Home)
            .Include(x => x.Records).ThenInclude(x => x.Tariff).ThenInclude(x => x!.UtilityGroup)
            .Where(x => x.Date < date && x.HomeId == homeId).OrderBy(x => x.Date).LastOrDefaultAsync();
    }

    private static decimal CalculateMeters(Record record, Tariff tariff)
    {
        if (!tariff.UseLimits)
        {
            return record.Difference * tariff.Cost;
        }

        var limits = tariff.Limits.OrderBy(x => x.Limit);
        if (record.Measure < limits.First().Limit)
        {
            return record.Difference * tariff.Cost;
        }
        else
        {
            List<(decimal Cost, decimal Measure)> limitedMeasures = [];
            var tmpMeasure = record.Difference;
            foreach (var limit in limits)
            {
                if ((tmpMeasure + limitedMeasures.Sum(x => x.Measure)) > limit.Limit)
                {
                    limitedMeasures.Add(new(limit.CostAfterLimit, limit.Limit));
                    tmpMeasure -= limit.Limit;
                }
                else
                {
                    limitedMeasures.Add(new(limit.CostAfterLimit, tmpMeasure));
                }
            }

            return limitedMeasures.Sum(x => x.Measure * x.Cost);
        }
    }

    private async Task<decimal> GetPreviousMeasure(Guid utilityId, Guid homeId)
    {
        var previousRecord = await context.Checks
            .Where(x => x.HomeId == homeId)
            .OrderBy(x => x.Date)
            .SelectMany(x => x.Records)
            .Where(x => x.Tariff!.UtilityGroupId == utilityId && x.Measure != 0)
            .LastOrDefaultAsync();
        return previousRecord is null ? 0 : previousRecord.Measure;
    }
}