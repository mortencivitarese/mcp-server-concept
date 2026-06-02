using System.ComponentModel;
using ModelContextProtocol.Server;
using RcMcpAtea.Services;

namespace RcMcpAtea;

[McpServerToolType]
public class RcMcpAteaTool
{
    private readonly RcMcpAteaService _service;

    public RcMcpAteaTool(RcMcpAteaService service)
    {
        _service = service;
    }

    [McpServerTool, Description("Get detailed information about a country — capital, population, currency, languages and more.")]
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

    [McpServerTool, Description("Get current weather for any city in the world.")]
    public async Task<string> GetWeather(
        [Description("City name, e.g. 'Copenhagen', 'London', 'Tokyo'")] string city)
    {
        return await _service.GetWeatherAsync(city);
    }

    [McpServerTool, Description("Get live currency exchange rates for a base currency.")]
    public async Task<string> GetExchangeRates(
        [Description("Base currency code, e.g. 'DKK', 'EUR', 'USD'")] string baseCurrency)
    {
        return await _service.GetExchangeRatesAsync(baseCurrency);
    }

    [McpServerTool, Description("Get information about a Pokemon by name or number.")]
    public async Task<string> GetPokemon(
        [Description("Pokemon name or number, e.g. 'pikachu', 'charizard', '25'")] string name)
    {
        return await _service.GetPokemonAsync(name);
    }

    [McpServerTool, Description("Search for books by title using Open Library.")]
    public async Task<string> GetBook(
        [Description("Book title or keywords, e.g. 'Harry Potter', 'Lord of the Rings'")] string title)
    {
        return await _service.GetBookAsync(title);
    }

    [McpServerTool, Description("Get geographic location and ISP info for an IP address.")]
    public async Task<string> GetIpInfo(
        [Description("IP address, e.g. '8.8.8.8'")] string ip)
    {
        return await _service.GetIpInfoAsync(ip);
    }
}
