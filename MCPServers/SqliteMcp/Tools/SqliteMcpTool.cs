using System.ComponentModel;
using ModelContextProtocol.Server;
using SqliteMcp.Services;

namespace SqliteMcp;

[McpServerToolType]
public class SqliteMcpTool(SqliteMcpService service)
{
    [McpServerTool, Description("List all tables in the database with their schema.")]
    public Task<string> ListTables()
        => service.ListTablesAsync();

    [McpServerTool, Description("Get rows from a table. Tables: countries, products, employees.")]
    public Task<string> QueryTable(
        [Description("Table name: countries, products, or employees")] string table,
        [Description("Max rows to return, e.g. 10")] int limit = 10)
        => service.QueryTableAsync(table, limit);

    [McpServerTool, Description("Search for a value in a specific column of a table.")]
    public Task<string> Search(
        [Description("Table name: countries, products, or employees")] string table,
        [Description("Column to search in, e.g. 'name', 'category', 'department'")] string column,
        [Description("Value to search for, e.g. 'Denmark', 'Electronics', 'Engineering'")] string value)
        => service.SearchAsync(table, column, value);

    [McpServerTool, Description("Get row count summary for a table.")]
    public Task<string> GetSummary(
        [Description("Table name: countries, products, or employees")] string table)
        => service.GetSummaryAsync(table);
}
