using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace SqliteMcp.Services;

public class SqliteMcpService
{
    private readonly string _dbPath;
    private readonly ILogger<SqliteMcpService> _logger;

    public SqliteMcpService(IConfiguration configuration, ILogger<SqliteMcpService> logger)
    {
        _logger = logger;
        _dbPath = configuration["Sqlite:DbPath"]
            ?? Path.Combine(AppContext.BaseDirectory, "Data", "sample.db");
    }

    private async Task<string> QueryAsync(string sql, Dictionary<string, object>? parameters = null)
    {
        await using var conn = new SqliteConnection($"Data Source={_dbPath};Mode=ReadOnly");
        await conn.OpenAsync();
        await using var cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        if (parameters != null)
            foreach (var p in parameters)
                cmd.Parameters.AddWithValue(p.Key, p.Value);

        var rows = new List<Dictionary<string, object?>>();
        await using var reader = await cmd.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            var row = new Dictionary<string, object?>();
            for (int i = 0; i < reader.FieldCount; i++)
                row[reader.GetName(i)] = reader.IsDBNull(i) ? null : reader.GetValue(i);
            rows.Add(row);
        }
        return JsonSerializer.Serialize(rows, new JsonSerializerOptions { WriteIndented = true });
    }

    public Task<string> ListTablesAsync() =>
        QueryAsync("SELECT name, sql FROM sqlite_master WHERE type='table' ORDER BY name");

    public Task<string> QueryTableAsync(string table, int limit) =>
        QueryAsync($"SELECT * FROM [{table}] LIMIT {Math.Clamp(limit,1,100)}");

    public Task<string> SearchAsync(string table, string column, string value) =>
        QueryAsync($"SELECT * FROM [{table}] WHERE [{column}] LIKE @val LIMIT 50",
            new() { ["@val"] = $"%{value}%" });

    public Task<string> GetSummaryAsync(string table) =>
        QueryAsync($"SELECT COUNT(*) as total_rows FROM [{table}]");
}
