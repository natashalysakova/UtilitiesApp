using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

public class UtilitiesDbContextFactory : IDesignTimeDbContextFactory<UtilitiesDbContext>
{
    public UtilitiesDbContext CreateDbContext(string[] args)
    {
        var builder = DistributedApplication.CreateBuilder(args);

        var mysql = builder.AddMySql("tmp")
                   .AddDatabase("tmpdb");

        var optionsBuilder = new DbContextOptionsBuilder<UtilitiesDbContext>();
        var connection = "Server=tmp;Port=3306;Uid=root;Pwd=67cYdL6M7V45Drt7FQn4Hc;Database=tmpdb";
        optionsBuilder.UseMySql(connection, ServerVersion.Parse("11.5.2-mariadb"), options =>
        {
            options.EnableRetryOnFailure();
        });
        return new UtilitiesDbContext(optionsBuilder.Options);
    }
}