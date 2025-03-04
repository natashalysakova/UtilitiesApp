using System.Text.Json.Serialization;
using ApiService.ValidationResultFactory;
using FluentValidation;
using Infrastructure;
using Infrastructure.Services;
using Infrastructure.Validation;
using Microsoft.AspNetCore.Mvc;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add service defaults & Aspire client integrations.
        builder.AddServiceDefaults();

        builder.Services
            .AddControllers(o =>
                {
                    o.Filters.Add(new ProducesResponseTypeAttribute(typeof(ErrorResult), 500));
                })
            .AddJsonOptions(options =>
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

        builder.Services.AddEndpointsApiExplorer();

        // Add services to the container.
        builder.Services.AddProblemDetails();

        var dbName = "UtilitiesDb";
        builder.AddMySqlDbContext<UtilitiesDbContext>(
            connectionName: dbName,
            configureDbContextOptions: (options) =>
            {
                UtilitiesDbContext.AddInterceptors(options);
            });

        builder.Services.AddScoped<CheckCalculationService>();

        builder.Services.AddValidatorsFromAssemblyContaining<HomeValidator>();
        builder.Services.AddFluentValidationAutoValidation(config =>
        {
            config.DisableBuiltInModelValidation = true;
            config.OverrideDefaultResultFactoryWith<ValidationResultFactory>();
        });

        builder.Services.AddOpenApiDocument();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        app.UseExceptionHandler();

        app.MapDefaultEndpoints();
        app.MapControllers();

        app.UseOpenApi();
        app.UseSwaggerUi();

        app.Run();
    }
}