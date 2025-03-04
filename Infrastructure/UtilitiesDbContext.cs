using Infrastructure.Interceptors;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class UtilitiesDbContext(DbContextOptions<UtilitiesDbContext> options) : DbContext(options)
{
    public DbSet<Home> Homes { get; set; }

    public DbSet<Tariff> Tariffs { get; set; }

    public DbSet<Check> Checks { get; set; }

    public DbSet<Record> Records { get; set; }

    public DbSet<TariffLimit> Limits { get; set; }

    public DbSet<UtilityGroup> UtilityGroups { get; set; }

    public static void AddInterceptors(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(new SoftDeleteInterceptor());
        optionsBuilder.AddInterceptors(new AuditInterceptor());
        optionsBuilder.AddInterceptors(new SetAsDefaultInterceptor());
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public IQueryable<Check> GetChecksWithDetails(bool includeDetails)
    {
        var checks = includeDetails ?
            Checks
                .Include(x => x.Records).ThenInclude(x => x.Tariff).ThenInclude(x => x!.UtilityGroup)
                .Include(x => x.Records.OrderBy(x => x.Tariff!.UtilityGroup!.Priority))
                .Include(x => x.Records).ThenInclude(x => x.Tariff).ThenInclude(x => x!.Home)
                .Include(x => x.Home) :
            Checks.AsQueryable();
        return checks.OrderBy(x => x.CreatedAt);
    }

    public IQueryable<Home> GetHomesWithDetails(bool includeDetails)
    {
        var homes = includeDetails ?
                Homes
                    .Include(x => x.Tariffs).ThenInclude(x => x.Limits)
                    .Include(x => x.Checks)
                        .ThenInclude(x => x.Records)
                            .ThenInclude(x => x.Tariff)
                                .ThenInclude(x => x!.UtilityGroup) :
                Homes.AsQueryable();
        return homes.OrderBy(x => x.CreatedAt);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Home>().HasQueryFilter(p => !p.IsArchived);

        modelBuilder.Entity<Check>().HasQueryFilter(p => !p.IsArchived);
        modelBuilder.Entity<Record>().HasQueryFilter(p => !p.IsArchived);

        modelBuilder.Entity<UtilityGroup>().HasQueryFilter(p => !p.IsArchived);

        modelBuilder.Entity<Tariff>().HasQueryFilter(p => !p.IsArchived);
        modelBuilder.Entity<TariffLimit>()
            .HasQueryFilter(p => !p.IsArchived)
            .HasIndex(p => new { p.TariffId, p.Limit }).IsUnique();
    }
}