using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Infrastructure.Interceptors;

public class SoftDeleteInterceptor : UtilityDbSaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        if (eventData.Context is null)
        {
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        var toBeDeleted = eventData.Context.ChangeTracker
            .Entries<IArchivableEntity>()
            .Where(x => x.State == EntityState.Deleted);

        foreach (var entry in toBeDeleted)
        {
            entry.State = EntityState.Modified;
            entry.Entity.IsArchived = true;
        }

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}
