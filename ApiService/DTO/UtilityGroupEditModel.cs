namespace ApiService.DTO;

public class UtilityGroupEditModel
{
    public required Guid Id { get; set; }

    public required string Name { get; set; }

    public int Priority { get; set; }
}
