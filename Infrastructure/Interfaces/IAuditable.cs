using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Interfaces;

public interface IAuditable
{
    DateTime CreatedAt { get; set; }

    DateTime? ModifiedAt { get; set; }

    DateTime? DeletedAt { get; set; }

    void Audit(EntityState state)
    {
        switch (state)
        {
            case EntityState.Deleted:
                DeletedAt = DateTime.UtcNow;
                break;
            case EntityState.Modified:
                ModifiedAt = DateTime.UtcNow;
                break;
            case EntityState.Added:
                CreatedAt = DateTime.UtcNow;
                break;
            default:
                break;
        }
    }
}
