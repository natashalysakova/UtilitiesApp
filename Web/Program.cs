using System.Globalization;
using ApexCharts;
using Microsoft.AspNetCore.Localization;
using MudBlazor.Services;
using Web.Clients;
using Web.Components;
using Web.NavigationServices;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

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

CultureInfo.DefaultThreadCurrentCulture = new("uk-UA");
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[]
    {
            new CultureInfo("uk-UA"),
            new CultureInfo("en"),
    };

    options.DefaultRequestCulture = new RequestCulture("uk-UA", "uk-UA");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});

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

app.Run();
