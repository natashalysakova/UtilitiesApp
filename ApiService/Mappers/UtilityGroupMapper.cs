using ApiService.DTO;
using Infrastructure.Models;
using Riok.Mapperly.Abstractions;

namespace ApiService.Mappers;

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public static partial class UtilityGroupMapper
{
    [MapperRequiredMapping(RequiredMappingStrategy.Source)]
    public static partial UtilityGroup ToEntity(this UtilityGroupViewModel dto);

    public static partial UtilityGroupViewModel ToViewDto(this UtilityGroup group);

    [MapperRequiredMapping(RequiredMappingStrategy.Source)]
    public static partial UtilityGroup ToEntity(this UtilityGroupEditModel dto);

    public static partial UtilityGroupEditModel ToEditDto(this UtilityGroup group);
}