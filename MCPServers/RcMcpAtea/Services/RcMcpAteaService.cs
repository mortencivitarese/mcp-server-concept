using MCPServers.Shared;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace RcMcpAtea.Services;

public class RcMcpAteaService : BaseHttpService
{
    private readonly string _baseUrl;
    private readonly string _weatherUrl;
    private readonly string _weatherGeoUrl;
    private readonly string _exchangeUrl;
    private readonly string _pokemonUrl;
    private readonly string _bookUrl;
    private readonly string _ipGeoUrl;

    public RcMcpAteaService(
        IConfiguration configuration,
        HttpClient client,
        ILogger<RcMcpAteaService> logger)
        : base(configuration, client, logger)
    {
        _baseUrl      = configuration["RcMcpAteaApi:BaseUrl"]    ?? throw new InvalidOperationException("RcMcpAteaApi:BaseUrl is not configured");
        _weatherUrl    = configuration["WeatherApi:BaseUrl"]      ?? throw new InvalidOperationException("WeatherApi:BaseUrl is not configured");
        _weatherGeoUrl = configuration["WeatherApi:GeocodingUrl"] ?? throw new InvalidOperationException("WeatherApi:GeocodingUrl is not configured");
        _exchangeUrl   = configuration["ExchangeRateApi:BaseUrl"] ?? throw new InvalidOperationException("ExchangeRateApi:BaseUrl is not configured");
        _pokemonUrl    = configuration["PokemonApi:BaseUrl"]      ?? throw new InvalidOperationException("PokemonApi:BaseUrl is not configured");
        _bookUrl       = configuration["BookApi:BaseUrl"]         ?? throw new InvalidOperationException("BookApi:BaseUrl is not configured");
        _ipGeoUrl      = configuration["IpGeoApi:BaseUrl"]        ?? throw new InvalidOperationException("IpGeoApi:BaseUrl is not configured");
    }

    public async Task<string> GetCountryByNameAsync(string name) =>
        await GetAsync($"{_baseUrl}/name/{Uri.EscapeDataString(name)}?fullText=false");

    public async Task<string> GetCountriesByRegionAsync(string region) =>
        await GetAsync($"{_baseUrl}/region/{Uri.EscapeDataString(region)}?fields=name,capital,population,currencies,flags");

    public async Task<string> GetCountriesByCurrencyAsync(string currency) =>
        await GetAsync($"{_baseUrl}/currency/{Uri.EscapeDataString(currency)}?fields=name,capital,population,flags");

    public async Task<string> GetWeatherAsync(string city)
    {
        var geo = await GetAsync($"{_weatherGeoUrl}/search?name={Uri.EscapeDataString(city)}&count=1");
        using var doc = System.Text.Json.JsonDocument.Parse(geo);
        var result = doc.RootElement.GetProperty("results")[0];
        var lat = result.GetProperty("latitude").GetDouble();
        var lon = result.GetProperty("longitude").GetDouble();
        var latStr = lat.ToString(System.Globalization.CultureInfo.InvariantCulture);
        var lonStr = lon.ToString(System.Globalization.CultureInfo.InvariantCulture);
        return await GetAsync($"{_weatherUrl}/forecast?latitude={latStr}&longitude={lonStr}&current_weather=true&wind_speed_unit=ms&temperature_unit=fahrenheit");
    }

    public async Task<string> GetExchangeRatesAsync(string baseCurrency) =>
        await GetAsync($"{_exchangeUrl}/latest/{Uri.EscapeDataString(baseCurrency.ToUpper())}");

    public async Task<string> GetPokemonAsync(string name) =>
        await GetAsync($"{_pokemonUrl}/pokemon/{Uri.EscapeDataString(name.ToLower())}");

    public async Task<string> GetBookAsync(string title) =>
        await GetAsync($"{_bookUrl}/search.json?q={Uri.EscapeDataString(title)}&limit=3&fields=title,author_name,first_publish_year,isbn");

    public async Task<string> GetIpInfoAsync(string ip) =>
        await GetAsync($"{_ipGeoUrl}/{Uri.EscapeDataString(ip)}");
}
