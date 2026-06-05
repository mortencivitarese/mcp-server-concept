using MCPServers.Shared;
using MCPServers.Shared.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace OvelseTilMth.Services;

public class OvelseTilMthService : BaseHttpService
{
    private const string WorkspaceId = "7a20dba1-b7c7-4350-b203-b9d2113e7918";
    private const string FabricScope = "https://api.fabric.microsoft.com/.default";
    private const string FabricBaseUrl = "https://api.fabric.microsoft.com/v1";

    private readonly TokenContextAccessor _tokenContextAccessor;
    private readonly TokenExchangeService _tokenExchangeService;

    public OvelseTilMthService(
        IConfiguration configuration,
        HttpClient client,
        ILogger<OvelseTilMthService> logger,
        TokenContextAccessor tokenContextAccessor,
        TokenExchangeService tokenExchangeService)
        : base(configuration, client, logger)
    {
        _tokenContextAccessor = tokenContextAccessor;
        _tokenExchangeService = tokenExchangeService;
    }

    public async Task<string> GetLakehousesAsync()
    {
        await SetAuthHeader();
        return await GetAsync($"{FabricBaseUrl}/workspaces/{WorkspaceId}/lakehouses");
    }

    public async Task<string> GetLakehouseTablesAsync(string lakehouseId)
    {
        await SetAuthHeader();
        return await GetAsync($"{FabricBaseUrl}/workspaces/{WorkspaceId}/lakehouses/{Uri.EscapeDataString(lakehouseId)}/tables");
    }

    public async Task<string> GetLakehouseAsync(string lakehouseId)
    {
        await SetAuthHeader();
        return await GetAsync($"{FabricBaseUrl}/workspaces/{WorkspaceId}/lakehouses/{Uri.EscapeDataString(lakehouseId)}");
    }

    private async Task SetAuthHeader()
    {
        var tokenContext = _tokenContextAccessor.TokenValidatedContext;
        var accessToken = await _tokenExchangeService.ExchangeTokenAsync(tokenContext, FabricScope);
        _client.DefaultRequestHeaders.Remove("Authorization");
        _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");
    }
}
