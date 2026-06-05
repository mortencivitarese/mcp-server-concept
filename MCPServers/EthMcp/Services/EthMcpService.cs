using MCPServers.Shared;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Text.Json;

namespace EthMcp.Services;

public class EthMcpService : BaseHttpService
{
    private readonly string _rpcUrl;

    public EthMcpService(IConfiguration configuration, HttpClient client, ILogger<EthMcpService> logger)
        : base(configuration, client, logger)
    {
        _rpcUrl = configuration["Eth:RpcUrl"] ?? throw new InvalidOperationException("Eth:RpcUrl not configured");
    }

    private async Task<string> RpcAsync(string method, object[] @params)
    {
        var body = JsonSerializer.Serialize(new { jsonrpc = "2.0", id = 1, method, @params });
        return await PostAsync(_rpcUrl, body);
    }

    public Task<string> GetBalanceAsync(string address) =>
        RpcAsync("eth_getBalance", [address, "latest"]);

    public Task<string> GetLatestBlockAsync() =>
        RpcAsync("eth_getBlockByNumber", ["latest", false]);

    public Task<string> GetBlockByNumberAsync(string number) =>
        RpcAsync("eth_getBlockByNumber", [number, false]);

    public Task<string> GetTransactionAsync(string hash) =>
        RpcAsync("eth_getTransactionByHash", [hash]);

    public Task<string> GetGasPriceAsync() =>
        RpcAsync("eth_gasPrice", []);

    public Task<string> GetChainIdAsync() =>
        RpcAsync("eth_chainId", []);
}
