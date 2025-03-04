using Infrastructure;
using MigrationService;

var builder = Host.CreateApplicationBuilder(args);
builder.Logging.AddConsole();
builder.AddServiceDefaults();
builder.Services.AddOpenTelemetry()
    .WithTracing(tracing => tracing.AddSource(Worker.ActivitySourceName));

builder.Services.AddHostedService<Worker>();

builder.AddMySqlDbContext<UtilitiesDbContext>(connectionName: "UtilitiesDb", configureDbContextOptions: (options) =>
{
    UtilitiesDbContext.AddInterceptors(options);
});
builder.EnrichMySqlDbContext<UtilitiesDbContext>();

var host = builder.Build();
host.Run();
