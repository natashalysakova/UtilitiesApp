var builder = DistributedApplication.CreateBuilder(args);
var dbName = "UtilitiesDb";

var databaseResource = builder.AddMySql("mysql")
                   .WithDataVolume()
                   .WithPhpMyAdmin()
                   .WithLifetime(ContainerLifetime.Persistent);

var database = databaseResource.AddDatabase(dbName);

var migration = builder.AddProject<Projects.MigrationService>("migrationservice")
    .WithReference(database)
    .WaitFor(database);

var apiService = builder.AddProject<Projects.ApiService>("apiservice")
    .WithReference(database)
    .WaitFor(database)
    .WaitForCompletion(migration, 0);

builder.AddProject<Projects.Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(apiService)
    .WaitFor(apiService);

builder.Build().Run();
