using SqliteMcp;
using SqliteMcp.Services;
using MCPServers.Shared.Extensions;
using MCPServers.Shared.Services;

var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsDevelopment())
{
    builder.Logging.AddConsole();
    builder.Logging.AddDebug();
}

var configuration = MCPServers.Shared.Extensions.ConfigurationExtensions.BuildMcpConfiguration(includeEnvironmentVariables: true);
builder.Services.AddSingleton<IConfiguration>(configuration);

builder.Services.AddHttpClient();
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient());

builder.Services.AddScoped<SqliteMcpService>();

builder.Services.AddMcpOpenTelemetry(builder.Logging);

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<TokenContextAccessor>();
builder.Services.AddMcpAuthentication(
    configuration,
    onTokenValidated: context =>
    {
        var tokenContextAccessor = context.HttpContext.RequestServices
            .GetRequiredService<TokenContextAccessor>();
        tokenContextAccessor.SetTokenValidatedContext(context);
        return Task.CompletedTask;
    },
    validateIssuer: true
);

builder.Services.AddAuthorization();

var isTransportStateless = bool.Parse(configuration["IsTransportStateless"] ?? "true");
builder.Services.AddMcpServer()
    .WithHttpTransport(options => options.Stateless = isTransportStateless)
    .WithTools<SqliteMcpTool>();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

var serverUrl = configuration["ServerUrl"] ?? "http://0.0.0.0:4551";

app.MapMcp().RequireAuthorization();

app.Run(serverUrl);
