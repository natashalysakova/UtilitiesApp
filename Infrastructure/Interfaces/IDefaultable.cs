namespace Infrastructure.Interfaces;

public interface IDefaultable
{
    bool IsDefault { get; set; }

    public Guid Scope { get; set; }
}
