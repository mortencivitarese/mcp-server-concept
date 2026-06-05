using System.ComponentModel;
using ModelContextProtocol.Server;
using EthMcp.Services;

namespace EthMcp;

[McpServerToolType]
public class EthMcpTool(EthMcpService service)
{
    [McpServerTool, Description("Get ETH balance for an Ethereum wallet address (in Wei).")]
    public Task<string> GetBalance([Description("Ethereum address, e.g. '0xd8dA6BF26964aF9D7eEd9e03E53415D37aA96045'")] string address)
        => service.GetBalanceAsync(address);

    [McpServerTool, Description("Get the latest Ethereum block info.")]
    public Task<string> GetLatestBlock()
        => service.GetLatestBlockAsync();

    [McpServerTool, Description("Get an Ethereum block by block number (hex or decimal).")]
    public Task<string> GetBlock([Description("Block number as hex e.g. '0x134E820' or 'latest'")] string number)
        => service.GetBlockByNumberAsync(number);

    [McpServerTool, Description("Get details about an Ethereum transaction by hash.")]
    public Task<string> GetTransaction([Description("Transaction hash, e.g. '0xabc123...'")] string hash)
        => service.GetTransactionAsync(hash);

    [McpServerTool, Description("Get current Ethereum gas price in Wei.")]
    public Task<string> GetGasPrice()
        => service.GetGasPriceAsync();

    [McpServerTool, Description("Get the Ethereum chain ID (1 = mainnet).")]
    public Task<string> GetChainId()
        => service.GetChainIdAsync();
}
