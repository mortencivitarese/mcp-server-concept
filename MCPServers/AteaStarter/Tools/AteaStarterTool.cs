using System.ComponentModel;
using ModelContextProtocol.Server;
using AteaStarter.Services;

namespace AteaStarter;

[McpServerToolType]
public class AteaStarterTool
{
    private readonly AteaStarterService _service;

    public AteaStarterTool(AteaStarterService service)
    {
        _service = service;
    }

    [McpServerTool, Description("Get detailed information about a country — capital, population, currency, languages, flag and more.")]
    public async Task<string> GetCountryInfo(
        [Description("Country name in English, e.g. 'Denmark', 'Germany', 'Japan'")] string countryName)
    {
        return await _service.GetCountryByNameAsync(countryName);
    }

    [McpServerTool, Description("List all countries in a geographic region with capital, population and currency.")]
    public async Task<string> GetCountriesByRegion(
        [Description("Region name: Africa, Americas, Asia, Europe, Oceania")] string region)
    {
        return await _service.GetCountriesByRegionAsync(region);
    }

    [McpServerTool, Description("Find all countries that use a specific currency.")]
    public async Task<string> GetCountriesByCurrency(
        [Description("Currency code or name, e.g. 'EUR', 'USD', 'DKK'")] string currency)
    {
        return await _service.GetCountriesByCurrencyAsync(currency);
    }
}
