using System.ComponentModel;
using ModelContextProtocol.Server;
using CryptoMcp.Services;

namespace CryptoMcp;

[McpServerToolType]
public class CryptoMcpTool(CryptoMcpService service)
{
    [McpServerTool, Description("Get current price for a trading pair from Binance, e.g. BTCUSDT, ETHUSDT, SOLUSDT.")]
    public Task<string> GetPrice([Description("Trading pair symbol, e.g. 'BTCUSDT', 'ETHUSDT', 'BNBUSDT'")] string symbol)
        => service.GetPriceAsync(symbol);

    [McpServerTool, Description("Get top N cryptocurrencies by market cap from CoinGecko.")]
    public Task<string> GetTopCoins([Description("Number of coins to return, e.g. 10")] int count = 10)
        => service.GetTopCoinsAsync(count);

    [McpServerTool, Description("Get detailed info about a specific coin from CoinGecko.")]
    public Task<string> GetCoinInfo([Description("Coin ID, e.g. 'bitcoin', 'ethereum', 'solana'")] string id)
        => service.GetCoinInfoAsync(id);

    [McpServerTool, Description("Get global crypto market overview — total market cap, volume, dominance.")]
    public Task<string> GetGlobalMarket()
        => service.GetGlobalMarketAsync();
}
