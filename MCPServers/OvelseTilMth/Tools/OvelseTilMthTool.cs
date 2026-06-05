using System.ComponentModel;
using ModelContextProtocol.Server;
using OvelseTilMth.Services;

namespace OvelseTilMth;

[McpServerToolType]
public class OvelseTilMthTool
{
    private readonly OvelseTilMthService _service;

    public OvelseTilMthTool(OvelseTilMthService service)
    {
        _service = service;
    }

    [McpServerTool, Description("List alle lakehouses i OvelseTilMth Fabric workspace")]
    public async Task<string> GetLakehouses()
    {
        return await _service.GetLakehousesAsync();
    }

    [McpServerTool, Description("Hent detaljer om et specifikt lakehouse inkl. SQL endpoint connection info")]
    public async Task<string> GetLakehouse(
        [Description("ID på lakehouse (GUID)")] string lakehouseId)
    {
        return await _service.GetLakehouseAsync(lakehouseId);
    }

    [McpServerTool, Description("List alle tabeller i et lakehouse i OvelseTilMth workspace")]
    public async Task<string> GetLakehouseTables(
        [Description("ID på lakehouse (GUID)")] string lakehouseId)
    {
        return await _service.GetLakehouseTablesAsync(lakehouseId);
    }
}
