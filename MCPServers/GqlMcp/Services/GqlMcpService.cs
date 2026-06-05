using MCPServers.Shared;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Text.Json;

namespace GqlMcp.Services;

public class GqlMcpService : BaseHttpService
{
    private readonly string _countriesUrl;
    private readonly string _rickMortyUrl;
    private readonly string _starWarsUrl;

    public GqlMcpService(IConfiguration configuration, HttpClient client, ILogger<GqlMcpService> logger)
        : base(configuration, client, logger)
    {
        _countriesUrl = configuration["GraphQL:CountriesUrl"] ?? throw new InvalidOperationException("GraphQL:CountriesUrl not configured");
        _rickMortyUrl = configuration["GraphQL:RickMortyUrl"] ?? throw new InvalidOperationException("GraphQL:RickMortyUrl not configured");
        _starWarsUrl  = configuration["GraphQL:StarWarsUrl"]  ?? throw new InvalidOperationException("GraphQL:StarWarsUrl not configured");
    }

    private async Task<string> GraphQlAsync(string url, string query, object? variables = null)
    {
        var body = JsonSerializer.Serialize(new { query, variables });
        var content = new StringContent(body, Encoding.UTF8, "application/json");
        return await PostAsync(url, body);
    }

    public Task<string> GetCountryAsync(string code) => GraphQlAsync(_countriesUrl,
        "query($code:ID!){country(code:$code){name capital emoji currency languages{name}}}",
        new { code = code.ToUpper() });

    public Task<string> GetContinentAsync(string code) => GraphQlAsync(_countriesUrl,
        "query($code:ID!){continent(code:$code){name countries{name capital emoji}}}",
        new { code = code.ToUpper() });

    public Task<string> GetCharacterAsync(string name) => GraphQlAsync(_rickMortyUrl,
        "query($name:String!){characters(filter:{name:$name}){results{id name status species gender image origin{name}}}}",
        new { name });

    public Task<string> GetEpisodeAsync(int id) => GraphQlAsync(_rickMortyUrl,
        "query($id:ID!){episode(id:$id){name air_date episode characters{name}}}",
        new { id });

    public Task<string> GetStarWarsPersonAsync(string search) => GraphQlAsync(_starWarsUrl,
        "query($search:String){allPeople(filter:{name:$search}){people{name birthYear gender homeworld{name} filmConnection{films{title}}}}}",
        new { search });

    public Task<string> GetStarWarsFilmAsync(int episodeID) => GraphQlAsync(_starWarsUrl,
        "query($id:Int){allFilms(filter:{episodeID:$id}){films{title director releaseDate openingCrawl}}}",
        new { id = episodeID });
}
