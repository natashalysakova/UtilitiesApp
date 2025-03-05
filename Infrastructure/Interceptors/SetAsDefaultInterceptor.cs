using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Infrastructure.Interceptors;

public class SetAsDefaultInterceptor : UtilityDbSaveChangesInterceptor
{
    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        if (eventData.Context is not UtilitiesDbContext dbContext)
        {
            return await base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        foreach (var item in dbContext.ChangeTracker.Entries<Home>().Where(x => x.Entity.IsDefault == true))
        {
            if (item.Entity is not Home)
            {
                continue;
            }

            if (item.State is EntityState.Deleted || item.Entity.IsArchived)
            {
                var leftFome = await dbContext.Homes
                    .FirstOrDefaultAsync(x => x.Id != item.Entity.Id && x.Scope == item.Entity.Scope);

                if (leftFome != null)
                {
                    leftFome.IsDefault = true;
                }
            }
            else if (item.State is EntityState.Added or EntityState.Modified)
            {
                foreach (var house in dbContext.Homes.Where(x => x.Scope == item.Entity.Scope))
                {
                    house.IsDefault = false;
                }

                item.Entity.IsDefault = true;
            }
        }

        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}