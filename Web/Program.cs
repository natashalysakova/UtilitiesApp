using System.Resources;


using System.Globalization;
using System.Resources;
using ApexCharts;
using Microsoft.AspNetCore.Localization;
using MudBlazor.Services;
using Web.Clients;
using Web.Components;
using Web.Localization;
using Web.NavigationServices;

[assembly: NeutralResourcesLanguage("uk-UA", UltimateResourceFallbackLocation.MainAssembly)]

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddControllers();
builder.Services.AddMudServices();
builder.Services.AddApexCharts();

builder.Services.AddSingleton<MenuUpdateService>();
builder.Services.AddScoped<CheckCalculateService>();

builder.Services.AddHttpClient<ApiClient>(client =>
{
    // This URL uses "https+http://" to indicate HTTPS is preferred over HTTP.
    // Learn more about service discovery scheme resolution at https://aka.ms/dotnet/sdschemes.
    client.BaseAddress = new("https+http://apiservice");
});

builder.Services.AddLocalization();

string[] supportedCultures = LocalizationService.SupportedCultures.Select(x => x.Name).ToArray();
var localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture(supportedCultures[0])
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);
builder.Services.AddSingleton<ILocalizationService, LocalizationService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);

    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.MapDefaultEndpoints();
app.MapControllers();

app.UseRequestLocalization(localizationOptions);

app.Run();