using System.ComponentModel;
using ModelContextProtocol.Server;
using RssMcp.Services;

namespace RssMcp;

[McpServerToolType]
public class RssMcpTool(RssMcpService service)
{
    [McpServerTool, Description("Get top stories from Hacker News RSS feed.")]
    public Task<string> GetHackerNews([Description("Number of items to return, e.g. 5")] int count = 10)
        => service.GetHackerNewsAsync(count);

    [McpServerTool, Description("Get latest BBC World News headlines.")]
    public Task<string> GetBbcNews([Description("Number of headlines to return, e.g. 5")] int count = 10)
        => service.GetBbcNewsAsync(count);

    [McpServerTool, Description("Get latest GitHub releases for the Atea MCP repo.")]
    public Task<string> GetGithubReleases([Description("Number of releases to return, e.g. 5")] int count = 5)
        => service.GetGithubReleasesAsync(count);
}
