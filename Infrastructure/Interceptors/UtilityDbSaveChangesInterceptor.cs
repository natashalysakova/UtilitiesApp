using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Infrastructure.Interceptors;

public class UtilityDbSaveChangesInterceptor : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        var task = SavingChangesAsync(eventData, result);
        if (task.IsCompleted)
        {
            return task.Result;
        }

        return task.AsTask().GetAwaiter().GetResult();
    }
}
