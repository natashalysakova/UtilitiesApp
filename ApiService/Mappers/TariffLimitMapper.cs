using ApiService.DTO;
using Infrastructure.Models;
using Riok.Mapperly.Abstractions;

namespace ApiService.Mappers;

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public static partial class TariffLimitMapper
{
    public static partial LimitViewDto ToViewDto(this TariffLimit tariff);

    public static partial LimitEditDto ToEditDto(this TariffLimit tariff);

    [MapperRequiredMapping(RequiredMappingStrategy.Source)]
    public static partial TariffLimit ToEntity(this LimitEditDto dto);

    [MapperRequiredMapping(RequiredMappingStrategy.Source)]
    public static partial TariffLimit ToEntity(this LimitViewDto dto);
}
