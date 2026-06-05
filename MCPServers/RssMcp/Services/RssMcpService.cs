using MCPServers.Shared;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.ServiceModel.Syndication;
using System.Text.Json;
using System.Xml;

namespace RssMcp.Services;

public class RssMcpService : BaseHttpService
{
    private readonly string _hackerNewsUrl;
    private readonly string _bbcUrl;
    private readonly string _githubReleasesUrl;

    public RssMcpService(IConfiguration configuration, HttpClient client, ILogger<RssMcpService> logger)
        : base(configuration, client, logger)
    {
        _hackerNewsUrl    = configuration["Rss:HackerNewsUrl"]    ?? throw new InvalidOperationException("Rss:HackerNewsUrl not configured");
        _bbcUrl           = configuration["Rss:BbcUrl"]           ?? throw new InvalidOperationException("Rss:BbcUrl not configured");
        _githubReleasesUrl = configuration["Rss:GithubReleasesUrl"] ?? throw new InvalidOperationException("Rss:GithubReleasesUrl not configured");
    }

    private async Task<string> FetchRssAsync(string url, int count = 10)
    {
        var xml = await GetAsync(url);
        using var reader = XmlReader.Create(new StringReader(xml));
        var feed = SyndicationFeed.Load(reader);
        var items = feed.Items.Take(count).Select(i => new
        {
            title   = i.Title?.Text,
            summary = i.Summary?.Text?.Replace("<p>","").Replace("</p>","").Trim()?.Substring(0, Math.Min(200, i.Summary?.Text?.Length ?? 0)),
            link    = i.Links.FirstOrDefault()?.Uri?.ToString(),
            date    = i.PublishDate.ToString("yyyy-MM-dd HH:mm")
        });
        return JsonSerializer.Serialize(items, new JsonSerializerOptions { WriteIndented = true });
    }

    public Task<string> GetHackerNewsAsync(int count = 10) => FetchRssAsync(_hackerNewsUrl, count);
    public Task<string> GetBbcNewsAsync(int count = 10)    => FetchRssAsync(_bbcUrl, count);
    public Task<string> GetGithubReleasesAsync(int count = 10) => FetchRssAsync(_githubReleasesUrl, count);
}
