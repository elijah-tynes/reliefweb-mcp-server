using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.Server;
using ReliefWebMCP;
using System.ComponentModel;

// Create application builder
var builder = Host.CreateApplicationBuilder(args);

// Configure builder logging
builder.Logging.AddConsole(consoleLogOptions =>
{
    consoleLogOptions.LogToStandardErrorThreshold = LogLevel.Trace;
});

// Add MCP services to builder
builder.Services
    .AddMcpServer()
    .WithStdioServerTransport()
    .WithToolsFromAssembly();

builder.Services.AddSingleton<ReliefWebService>();
builder.Services.AddHttpClient();

// Run server
await builder.Build().RunAsync();