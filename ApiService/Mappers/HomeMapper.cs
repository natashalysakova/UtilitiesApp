using ApiService.DTO;
using Infrastructure.Models;
using Riok.Mapperly.Abstractions;

namespace ApiService.Mappers;

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public static partial class HomeMapper
{
    public static partial HomeViewDto ToViewDto(this Home check);

    public static partial HomeEditDto ToEditDto(this Home check);

    [MapperRequiredMapping(RequiredMappingStrategy.Source)]
    public static partial Home ToEntity(this HomeEditDto dto);
}
