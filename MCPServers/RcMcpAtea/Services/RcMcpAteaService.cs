using MCPServers.Shared;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace RcMcpAtea.Services;

public class RcMcpAteaService : BaseHttpService
{
    private readonly string _baseUrl;

    public RcMcpAteaService(
        IConfiguration configuration,
        HttpClient client,
        ILogger<RcMcpAteaService> logger)
        : base(configuration, client, logger)
    {
        _baseUrl = configuration["RcMcpAteaApi:BaseUrl"]
            ?? throw new InvalidOperationException("RcMcpAteaApi:BaseUrl is not configured");
    }

    public async Task<string> GetCountryByNameAsync(string name) =>
        await GetAsync($"{_baseUrl}/name/{Uri.EscapeDataString(name)}?fullText=false");

    public async Task<string> GetCountriesByRegionAsync(string region) =>
        await GetAsync($"{_baseUrl}/region/{Uri.EscapeDataString(region)}?fields=name,capital,population,currencies,flags");

    public async Task<string> GetCountriesByCurrencyAsync(string currency) =>
        await GetAsync($"{_baseUrl}/currency/{Uri.EscapeDataString(currency)}?fields=name,capital,population,flags");

    // Open-Meteo weather
    public async Task<string> GetWeatherAsync(string city)
    {
        var geo = await GetAsync($"https://geocoding-api.open-meteo.com/v1/search?name={Uri.EscapeDataString(city)}&count=1");
        using var doc = System.Text.Json.JsonDocument.Parse(geo);
        var result = doc.RootElement.GetProperty("results")[0];
        var lat = result.GetProperty("latitude").GetDouble();
        var lon = result.GetProperty("longitude").GetDouble();
        return await GetAsync($"https://api.open-meteo.com/v1/forecast?latitude={lat}&longitude={lon}&current_weather=true&wind_speed_unit=ms");
    }

    // Exchange rates
    public async Task<string> GetExchangeRatesAsync(string baseCurrency) =>
        await GetAsync($"https://open.er-api.com/v6/latest/{Uri.EscapeDataString(baseCurrency.ToUpper())}");

    // PokeAPI
    public async Task<string> GetPokemonAsync(string name) =>
        await GetAsync($"https://pokeapi.co/api/v2/pokemon/{Uri.EscapeDataString(name.ToLower())}");

    // Open Library
    public async Task<string> GetBookAsync(string title) =>
        await GetAsync($"https://openlibrary.org/search.json?q={Uri.EscapeDataString(title)}&limit=3&fields=title,author_name,first_publish_year,isbn");

    // IP Geolocation
    public async Task<string> GetIpInfoAsync(string ip) =>
        await GetAsync($"https://ipwho.is/{Uri.EscapeDataString(ip)}");
}
