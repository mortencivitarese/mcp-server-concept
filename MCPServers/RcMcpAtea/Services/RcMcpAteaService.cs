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
}
