using ApiService.DTO;
using Infrastructure.Models;
using Riok.Mapperly.Abstractions;

namespace ApiService.Mappers;

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
[UseStaticMapper(typeof(HomeMapper))]
[UseStaticMapper(typeof(UtilityGroupMapper))]
public static partial class TariffMapper
{
    public static partial TariffViewDto ToViewDto(this Tariff record);

    public static partial TariffEditDto ToEditDto(this Tariff record);

    [MapperRequiredMapping(RequiredMappingStrategy.Source)]
    [MapPropertyFromSource(nameof(TariffEditDto.AdditionalFeeCost), Use = nameof(MapAdditionalCost))]
    [MapPropertyFromSource(nameof(TariffEditDto.FixedPay), Use = nameof(MapFixedPayCost))]
    [MapPropertyFromSource(nameof(TariffEditDto.Limits), Use = nameof(MapLimits))]
    public static partial Tariff ToEntity(this TariffEditDto dto);

    [MapperRequiredMapping(RequiredMappingStrategy.Source)]
    [MapperIgnoreSource(nameof(TariffCalulateDto.HomeArea))]
    [MapPropertyFromSource(nameof(Tariff.UtilityGroup), Use = nameof(MapUtilityGroup))]
    public static partial Tariff ToEntity(this TariffCalulateDto dto);

    private static UtilityGroup MapUtilityGroup(TariffCalulateDto dto)
    {
        return new UtilityGroup() { Id = dto.UtilityGroupId, Name = dto.UtilityGroupName };
    }

    private static decimal? MapAdditionalCost(TariffEditDto dto)
    {
        return dto.UseAdditionalFee ? dto.AdditionalFeeCost : 0;
    }

    private static decimal? MapFixedPayCost(TariffEditDto dto)
    {
        return dto.UseFixedPay ? dto.FixedPay : 0;
    }

    private static ICollection<TariffLimit> MapLimits(TariffEditDto dto)
    {
        if (dto.UseLimits)
        {
            return dto.Limits.Select(x => x.ToEntity()).ToList();
        }

        return Array.Empty<TariffLimit>();
    }
}