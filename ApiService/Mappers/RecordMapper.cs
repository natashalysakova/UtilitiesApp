using ApiService.DTO;
using Infrastructure.Models;
using Riok.Mapperly.Abstractions;

namespace ApiService.Mappers;

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
[UseStaticMapper(typeof(TariffMapper))]

public static partial class RecordMapper
{
    public static partial RecordViewDto ToViewDto(this Record record);

    public static partial RecordEditDto ToEditDto(this Record record);

    [MapperRequiredMapping(RequiredMappingStrategy.Source)]
    public static partial Record ToEntity(this RecordEditDto dto);
}
