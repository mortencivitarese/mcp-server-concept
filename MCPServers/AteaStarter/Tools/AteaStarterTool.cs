using System.ComponentModel;
using ModelContextProtocol.Server;
using AteaStarter.Services;

namespace AteaStarter;

[McpServerToolType]
public class AteaStarterTool
{
    private readonly AteaStarterService _service;

    public AteaStarterTool(AteaStarterService service)
    {
        _service = service;
    }

    [McpServerTool, Description("TODO: Replace with your tool description")]
    public async Task<string> ExampleTool(
        [Description("TODO: Replace with your parameter description")] string input)
    {
        return await _service.GetDataAsync(input);
    }
}
