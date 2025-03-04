using Infrastructure;
using Infrastructure.Models;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using System.Diagnostics;

namespace MigrationService;

public class Worker(ILogger<Worker> logger, IServiceProvider serviceProvider, IHostApplicationLifetime hostApplicationLifetime) : BackgroundService
{
    public const string ActivitySourceName = "Migrations";
    private static readonly ActivitySource _activitySource = new(ActivitySourceName);
    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        using var activity = _activitySource.StartActivity("Migrating database", ActivityKind.Client);
        try
        {
            logger.LogInformation("Starting migration");
            using var scope = serviceProvider.CreateScope();
            var dbContext = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<UtilitiesDbContext>();
            logger.LogInformation("Checking db exist");
            await EnsureDatabaseAsync(dbContext, cancellationToken);
            logger.LogInformation("Run migration");
            await RunMigrationAsync(dbContext, cancellationToken);
            logger.LogInformation("Starting migration");
            await SeedDataAsync(dbContext, cancellationToken);
            logger.LogInformation("Migration completed");
        }
        catch (Exception ex)
        {
            activity?.AddException(ex);
            activity?.SetStatus(ActivityStatusCode.Error);
            logger.LogError(ex, "Error during migration: {ErrorMessage}", ex.Message);
            logger.LogInformation("Error exist");
            Environment.Exit(-1);
        }

        logger.LogInformation("Host stopped");
        hostApplicationLifetime.StopApplication();
    }

    private static async Task EnsureDatabaseAsync(UtilitiesDbContext dbContext, CancellationToken cancellationToken)
    {
        var dbCreator = dbContext.GetService<IRelationalDatabaseCreator>();

        var strategy = dbContext.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () =>
        {
            // Create the database if it does not exist.
            // Do this first so there is then a database to start a transaction against.
            if (!await dbCreator.ExistsAsync(cancellationToken))
            {
                await dbCreator.CreateAsync(cancellationToken);
            }
        });
    }

    private static async Task RunMigrationAsync(UtilitiesDbContext dbContext, CancellationToken cancellationToken)
    {
        var strategy = dbContext.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () =>
        {
            if (dbContext.Database.HasPendingModelChanges())
            {

                throw new Exception("Model has pending changes");
            }

            if (dbContext.Database.GetPendingMigrations().Any())
                await dbContext.Database.MigrateAsync(cancellationToken);
        });
    }


    private static async Task SeedDataAsync(UtilitiesDbContext dbContext, CancellationToken cancellationToken)
    {
        var homes = new[]
{
                new Home{ Area = 55 ,Building = "22",City = "Київ", Apartment = "01",Country = "Україна",Name = "Демо",Region = "Київська область",Street = "Хрещатик",IsDefault = true,},
            };

        if (!dbContext.Homes.Any())
        {
            dbContext.Homes.AddRange(homes);
        }
        var strategy = dbContext.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () =>
        {
            await dbContext.SaveChangesAsync(cancellationToken);
        });


        //utilities = dbContext.UtilityTypes.ToArray();
        homes = [.. dbContext.Homes];
        var tariffs = new[]
    {
            new Tariff
            {
                HomeId = homes[0].Id,
                UtilityGroup = new UtilityGroup()
                {
                    Name = "Електроенергія",
                    Priority = 1
                },
                Cost = 2.64M,
                StartDate = new DateTime(2022, 1, 1),
                UseLimits = true,
                TariffType = TariffType.Meters,
                Units = "кВт",
                Limits =
                [
                    new TariffLimit(){ Limit = 200, CostAfterLimit = 4.68M},
                    new TariffLimit(){ Limit = 400, CostAfterLimit = 6.68M},
                    new TariffLimit(){ Limit = 600, CostAfterLimit = 7.68M}
                ]
            },
            new Tariff {
                HomeId = homes[0].Id,
                UtilityGroup = new UtilityGroup()
                {
                    Name ="Холодна вода",
                    Priority = 2
                },
                Cost = 50.97M,
                StartDate = new DateTime(2022,1,1),
                Units = "куб.м",
                TariffType = TariffType.Meters,
                UseFixedPay = true,
                FixedPayName = "Обслуговування мережі",
                FixedPay = 15.32M,
            },
            new Tariff {
                HomeId = homes[0].Id,
                UtilityGroup = new UtilityGroup()
                {
                    Name = "Газопостачання",
                    Priority = 3,
                },
                Cost = 7.96M,
                StartDate = new DateTime(2022,1,1),
                Units = "куб.м",
                TariffType = TariffType.Meters,
                UseAdditionalFee = true,
                AdditionalFeeName = "Транспорт газу",
                AdditionalFeeCost = 1.788M,
            },
            new Tariff
            {
                HomeId = homes[0].Id,
                UtilityGroup = new UtilityGroup()
                {
                    Name = "Опалення МЗК",
                    Priority = 4,
                },
                StartDate = new DateTime(2022,1,1),
                TariffType = TariffType.NotFixedPayment,
                Cost = 0
            },
            new Tariff
            {
                Cost = 1,
                HomeId = homes[0].Id,
                StartDate = new DateTime(2022,1,1),
                UtilityGroup = new UtilityGroup()
                {
                    Name = "Інтернет",
                    Priority = 5
                },
                Units = "Гб",
                TariffType = TariffType.Volume
            },
            new Tariff {
                HomeId = homes[0].Id,
                UtilityGroup = new UtilityGroup()
                {
                    Name = "Прибудинкова територія",
                    Priority = 6,
                },
                Cost = 14.86M,
                StartDate = new DateTime(2022,1,1),
                TariffType = TariffType.HouseHoldArea,
            },
            new Tariff {
                HomeId = homes[0].Id,
                UtilityGroup = new UtilityGroup()
                {
                    Name = "Охорона",
                    Priority = 7,
                },
                Cost = 100M,
                StartDate = new DateTime(2022,1,1),
                TariffType = TariffType.Fixed,
            },
        };

        if (!dbContext.Tariffs.Any())
        {
            dbContext.Tariffs.AddRange(tariffs);
        }

        await strategy.ExecuteAsync(async () =>
        {
            await dbContext.SaveChangesAsync(cancellationToken);
        });

        tariffs = [.. dbContext.Tariffs];

        if (!dbContext.Checks.Any())
        {
            var checks = new List<Check>();
            var random = new Random();

            var checkDate = new DateTime(2022, 1, 1);
            var chekCalcService = new CheckCalculationService(dbContext);

            while (checkDate < DateTime.Today)
            {
                var records = new List<Record>();

                foreach (var tariff in tariffs)
                {
                    var previousMeasure = random.Next(0, 200);
                    var measure = random.Next(previousMeasure, 300);
                    records.Add(new Record()
                    {
                        TariffId = tariff.Id,
                        Tariff = tariff,
                        Measure = measure,
                        PreviousMeasure = previousMeasure,
                        Difference = measure - previousMeasure,
                        Cost = tariff.TariffType is TariffType.NotFixedPayment ? random.Next(1, 150) : 0,
                    });
                }

                var check = new Check
                {
                    Date = checkDate,
                    HomeId = homes[0].Id,
                    Amount = records.Sum(x => x.Cost),
                    Records = records,
                };

                await chekCalcService.Calculate(check);

                dbContext.Checks.Add(check);
                await strategy.ExecuteAsync(async () =>
                {
                    await dbContext.SaveChangesAsync(cancellationToken);
                });

                checkDate = checkDate.AddMonths(1);

                if (cancellationToken.IsCancellationRequested)
                {
                    return;
                }
            }
        }
    }
}
