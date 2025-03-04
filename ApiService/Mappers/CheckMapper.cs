using ApiService.DTO;
using Infrastructure.Models;
using Riok.Mapperly.Abstractions;

namespace ApiService.Mappers;

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
[UseStaticMapper(typeof(HomeMapper))]
[UseStaticMapper(typeof(TariffMapper))]
[UseStaticMapper(typeof(RecordMapper))]
public static partial class CheckMapper
{
    public static partial CheckViewDto ToViewDto(this Check record);

    public static partial CheckEditDto ToEditDto(this Check record);

    [MapperRequiredMapping(RequiredMappingStrategy.Source)]
    [MapperIgnoreSource(nameof(CheckEditDto.HomeArea))]
    public static partial Check ToEntity(this CheckEditDto dto);
}
