using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Infrastructure.Interceptors;

public class AuditInterceptor : UtilityDbSaveChangesInterceptor
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

        var entries = eventData.Context.ChangeTracker.Entries<IAuditable>();
        foreach (var entity in entries)
        {
            entity.Entity.Audit(entity.State);
        }

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}