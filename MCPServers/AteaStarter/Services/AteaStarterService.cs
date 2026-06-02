using MCPServers.Shared;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AteaStarter.Services;

public class AteaStarterService : BaseHttpService
{
    private readonly string _baseUrl;

    public AteaStarterService(
        IConfiguration configuration,
        HttpClient client,
        ILogger<AteaStarterService> logger)
        : base(configuration, client, logger)
    {
        _baseUrl = configuration["AteaStarterApi:BaseUrl"]
            ?? throw new InvalidOperationException("AteaStarterApi:BaseUrl is not configured");
    }

    public async Task<string> GetCountryByNameAsync(string name) =>
        await GetAsync($"{_baseUrl}/name/{Uri.EscapeDataString(name)}?fullText=false");

    public async Task<string> GetCountriesByRegionAsync(string region) =>
        await GetAsync($"{_baseUrl}/region/{Uri.EscapeDataString(region)}?fields=name,capital,population,currencies,flags");

    public async Task<string> GetCountriesByCurrencyAsync(string currency) =>
        await GetAsync($"{_baseUrl}/currency/{Uri.EscapeDataString(currency)}?fields=name,capital,population,flags");
}
