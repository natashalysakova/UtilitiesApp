using System.Globalization;
using Microsoft.Extensions.Localization;

namespace Web.Localization;

internal class LocalizationService(IStringLocalizer<Resources> localizer, ILogger<LocalizationService> logger) : ILocalizationService
{

    private static readonly CultureInfo[] supportedCultures =
    [
        new CultureInfo("uk-UA"),
        new CultureInfo("en-US"),
    ];

    public static CultureInfo[] SupportedCultures { get => supportedCultures; }

    public LocalizedString this[string name]
    {
        get
        {
            return CheckResult(localizer.GetString(name));
        }
    }

    public LocalizedString this[string name, params object[] arguments]
    {
        get
        {
            try
            {
                return CheckResult(localizer.GetString(name, arguments));
            }
            catch
            {
                return CheckResult(localizer.GetString(name));
            }
        }
    }

    public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
    {
        return localizer.GetAllStrings(includeParentCultures);
    }

    private LocalizedString CheckResult(LocalizedString result)
    {
        if (result.ResourceNotFound || string.IsNullOrEmpty(result.Value))
        {
            result = new LocalizedString(result.Name, "{" + result.Name + "}", result.ResourceNotFound, result.SearchedLocation);
            logger.LogWarning("Translation for '{name}' {state}. Fallback value {value}", result.Name, result.ResourceNotFound ? "not found" : "is empty", result.Value);
        }

        return result;
    }
}
