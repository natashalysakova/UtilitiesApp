using Infrastructure;
using Infrastructure.Models;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tests;

[TestClass]
internal class MigrationTest
{
    UtilitiesDbContext context;

    public MigrationTest()
    {
        var connection = new SqliteConnection("Filename=:memory:");
        connection.Open();
        var contextOptions = new DbContextOptionsBuilder<UtilitiesDbContext>()
                    .UseSqlite(connection)
                    .Options;
        context = new UtilitiesDbContext(contextOptions);

        if (context.Database.EnsureCreated())
        {

        }
    }

    [TestMethod]
    public void TestUpMigrtion()
    {
        context.GetService<IMigrator>().Migrate("20250131221951_AddZeroCheckField");
        var home = new Home() { Area = 42, Building = "1", City = "testCity", Country = "testCountry", Name = "testHome", Region = "testRegion", Street = "testStreet" };

        var tariff = new Tariff() { Cost = 32, Home = home, StartDate = DateTime.Now, TariffType = TariffType.Fixed };
    }
}