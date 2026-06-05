using MCPServers.Shared;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace CryptoMcp.Services;

public class CryptoMcpService : BaseHttpService
{
    private readonly string _binanceUrl;
    private readonly string _coingeckoUrl;

    public CryptoMcpService(IConfiguration configuration, HttpClient client, ILogger<CryptoMcpService> logger)
        : base(configuration, client, logger)
    {
        _binanceUrl   = configuration["Crypto:BinanceUrl"]   ?? throw new InvalidOperationException("Crypto:BinanceUrl not configured");
        _coingeckoUrl = configuration["Crypto:CoingeckoUrl"] ?? throw new InvalidOperationException("Crypto:CoingeckoUrl not configured");
    }

    public Task<string> GetPriceAsync(string symbol) =>
        GetAsync($"{_binanceUrl}/api/v3/ticker/price?symbol={Uri.EscapeDataString(symbol.ToUpper())}");

    public Task<string> GetTopCoinsAsync(int count) =>
        GetAsync($"{_coingeckoUrl}/api/v3/coins/markets?vs_currency=usd&order=market_cap_desc&per_page={count}&page=1&sparkline=false");

    public Task<string> GetCoinInfoAsync(string id) =>
        GetAsync($"{_coingeckoUrl}/api/v3/coins/{Uri.EscapeDataString(id.ToLower())}?localization=false&tickers=false&market_data=true&community_data=false&developer_data=false");

    public Task<string> GetGlobalMarketAsync() =>
        GetAsync($"{_coingeckoUrl}/api/v3/global");
}
