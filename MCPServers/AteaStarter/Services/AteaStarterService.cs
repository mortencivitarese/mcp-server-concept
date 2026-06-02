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

    public async Task<string> GetDataAsync(string input)
    {
        var url = $"{_baseUrl}/TODO-replace-with-endpoint/{Uri.EscapeDataString(input)}";
        return await GetAsync(url);
    }
}
