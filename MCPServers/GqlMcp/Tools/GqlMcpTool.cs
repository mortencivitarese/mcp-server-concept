using System.ComponentModel;
using ModelContextProtocol.Server;
using GqlMcp.Services;

namespace GqlMcp;

[McpServerToolType]
public class GqlMcpTool(GqlMcpService service)
{
    [McpServerTool, Description("Get country details via GraphQL — name, capital, currency, emoji flag, languages.")]
    public Task<string> GetCountry([Description("ISO 2-letter country code, e.g. 'DK', 'DE', 'US'")] string code)
        => service.GetCountryAsync(code);

    [McpServerTool, Description("Get all countries in a continent via GraphQL.")]
    public Task<string> GetContinent([Description("Continent code: EU, NA, AS, AF, OC, SA, AN")] string code)
        => service.GetContinentAsync(code);

    [McpServerTool, Description("Search for a Rick & Morty character by name.")]
    public Task<string> GetCharacter([Description("Character name, e.g. 'Rick', 'Morty', 'Beth'")] string name)
        => service.GetCharacterAsync(name);

    [McpServerTool, Description("Get details about a Rick & Morty episode by number.")]
    public Task<string> GetEpisode([Description("Episode number, e.g. 1")] int id)
        => service.GetEpisodeAsync(id);

    [McpServerTool, Description("Search for a Star Wars person by name.")]
    public Task<string> GetStarWarsPerson([Description("Name, e.g. 'Luke', 'Darth Vader'")] string search)
        => service.GetStarWarsPersonAsync(search);

    [McpServerTool, Description("Get a Star Wars film by episode number (1-6).")]
    public Task<string> GetStarWarsFilm([Description("Episode number 1-6")] int episodeId)
        => service.GetStarWarsFilmAsync(episodeId);
}
