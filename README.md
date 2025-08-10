# ReliefWeb MCP Server
A Model Context Protocol (MCP) server for the [ReliefWeb](https://reliefweb.int/) humanitarian information service. 

Provided by the United Nations Office for the Coordination of Humanitarian Affairs (OCHA), ReliefWeb delivers fast, reliable updates from 4,000+ sources - including NGOs, governments and research centers to the humanitarian community. This MCP server seeks to enable natural language access via AI agents to critical humanitarian data such as disaster reports, job/volunteer opportunities, training programs, and resource updates - allowing users to quickly find, filter, and utilize up-to-date information essential for ongoing humanitarian efforts.

### Available Tools
- `GetReports`  
  - Retrieves update and situation reports curated from ReliefWeb
  - Optional inputs:
    - `keywords (string[])`: Keywords to narrow down the report search (default: all)
    - `numResults (int)`: Number of reports to return (default: 20)    
      You can also set the environment variable `REPORTS_REQUEST_SIZE` to override this
  - Returns: String collection of ReliefWeb reports sorted by recency
      
- `GetDisasters`     
  - Retrieves ReliefWeb information related to disasters
  - Optional inputs:
    - `keywords (string[])`: Keywords to narrow down the disasters search (default: all)
    - `numResults (int)`: Number of disasters to return (default: 20)    
      You can also set the environment variable `DISASTERS_REQUEST_SIZE` to override this
   - Returns: String collection of disasters sorted by recency
  
- `GetJobs`    
  - Retrieves humanitarian jobs and volunteer opportunities
  - Optional inputs:
    - `keywords (string[])`: Keywords to narrow down the jobs search (default: all)
    - `numResults (int)`: Number of jobs to return (default: 20)   
      You can also set the environment variable `JOBS_REQUEST_SIZE` to override this
  - Returns: String collection of jobs sorted by recency

- `GetTrainings`    
  - Retrieves training opportunities and courses for useful and necessary humanitarian skills
  - Optional inputs:
    - `keywords (string[])`: Keywords to narrow down the trainings search (default: all)
    - `numResults (int)`: Number of trainings to return (default: 20)    
      You can also set the environment variable `TRAININGS_REQUEST_SIZE` to override this
  - Returns: String collection of trainings sorted by recency
    
- `GetBlogs`    
  - Retrieves blog posts about ideas to grow and improve ReliefWeb
  - Optional inputs:
    - `keywords (string[])`: Keywords to narrow down the blogs search (default: all)
    - `numResults (int)`: Number of blogs to return (default: 20)    
      You can also set the environment variable `BLOGS_REQUEST_SIZE` to override this
  - Returns: String collection of blogs sorted by recency
  
- `GetResources`    
  - Retrieves static information about the ReliefWeb site and humanitarian resources
  - Optional inputs:
    - `keywords (string[])`: Keywords to narrow down the resources search (default: all)
    - `numResults (int)`: Number of resources to return (default: 20)    
      You can also set the environment variable `RESOURCES_REQUEST_SIZE` to override this
  - Returns: String collection of resources sorted by recency
     
## Prerequisites

- MCP host (such as [Visual Studio Code](https://code.visualstudio.com/))
- [Install Node.js](https://nodejs.org/)

## Getting Started

### Usage With Claude Desktop
Add the following to your `claude_desktop_config.json`, which can be found by navigating to `Claude Desktop` > `Settings` > `Developer` > `Edit Config`:

```JSON
{
  "mcpServers": {
    "reliefweb": {
      "command": "npx",
      "args": [
        "-y",
        "@elijahtynes/reliefweb-mcp-server"
      ],
      "env": {
        "REPORTS_REQUEST_SIZE": "[Replace with number for reports query size (Optional)]",
        "DISASTERS_REQUEST_SIZE": "[Replace with number for disasters query size (Optional)]",
        "JOBS_REQUEST_SIZE": "[Replace with number for jobs query size (Optional)]",
        "TRAININGS_REQUEST_SIZE": "[Replace with number for trainings query size (Optional)]",
        "BLOGS_REQUEST_SIZE": "[Replace with number for blogs query size (Optional)]",
        "RESOURCES_REQUEST_SIZE": "[Replace with number for resources query size (Optional)]"
      }
    }
  }
}
```
### Usage with Cursor
Navigate to `Cursor` > `Settings` > `Cursor Settings` > `MCP` > `Add a New MCP Server` and add the following content to the file:

```JSON
{
  "mcpServers": {
    "reliefweb": {
      "command": "npx",
      "args": ["-y", "@elijahtynes/reliefweb-mcp-server"],
      "env": {
        "REPORTS_REQUEST_SIZE": "[Replace with number for reports query size (Optional)]",
        "DISASTERS_REQUEST_SIZE": "[Replace with number for disasters query size (Optional)]",
        "JOBS_REQUEST_SIZE": "[Replace with number for jobs query size (Optional)]",
        "TRAININGS_REQUEST_SIZE": "[Replace with number for trainings query size (Optional)]",
        "BLOGS_REQUEST_SIZE": "[Replace with number for blogs query size (Optional)]",
        "RESOURCES_REQUEST_SIZE": "[Replace with number for resources query size (Optional)]"
      }
    }
  }
}
```

### Usage With VS Code
Add the following content to your `User Settings (JSON)` file in VS Code, which can be found at `Ctrl` + `Shift` + `P` > `Preferences: Open Settings (JSON)`:

> Note: The content can instead be added to a file named `.vscode/mcp.json` in your workspace, allowing you to share the configuration with others

```JSON
{
  "mcp": {
    "servers": {
      "reliefweb": {
        "command": "npx",
        "args": [
          "-y",
          "@elijahtynes/reliefweb-mcp-server"
        ],
        "env": {
          "REPORTS_REQUEST_SIZE": "[Replace with number for reports query size (Optional)]",
          "DISASTERS_REQUEST_SIZE": "[Replace with number for disasters query size (Optional)]",
          "JOBS_REQUEST_SIZE": "[Replace with number for jobs query size (Optional)]",
          "TRAININGS_REQUEST_SIZE": "[Replace with number for trainings query size (Optional)]",
          "BLOGS_REQUEST_SIZE": "[Replace with number for blogs query size (Optional)]",
          "RESOURCES_REQUEST_SIZE": "[Replace with number for resources query size (Optional)]"
        }
      }
    }
  }
}
```


## Code Samples

### ModelContextProtocol C#
```C#
using ModelContextProtocol.Client;

// Create STDIO transport using ReliefWeb MCP
var clientTransport = new StdioClientTransport(new StdioClientTransportOptions
{
    Name = "ReliefWebMCP",
    Command = "npx",
    Arguments = ["-y", "@elijahtynes/reliefweb-mcp-server"]
});

// Create client with transport
var mcpClient = await McpClientFactory.CreateAsync(clientTransport);

// Retrieve the list of tools available on the ReliefWebMCP server
var tools = await mcpClient.ListToolsAsync().ConfigureAwait(false);

foreach (var tool in tools)
{
    Console.WriteLine($"{tool.Name}");
}
```

```bash
GetDisasters
GetTrainings
GetBlogs
GetReports
GetJobs
GetResources
```

### Semantic_Kernel MCP Python
```Python
from semantic_kernel import Kernel
from semantic_kernel.connectors.mcp import MCPStdioPlugin

# Create kernel
kernel = Kernel()

# Create ReliefWeb MCP plugin over STDIO
async with MCPStdioPlugin(
    name="ReliefWebMCP",
    command="npx",
    args=["-y", "@elijahtynes/reliefweb-mcp-server"]
) as reliefweb_plugin:
    
    # Add ReliefWeb MCP tools to kernel plugins
    kernel.add_plugin(reliefweb_plugin)

    # Retrieve MCP tools
    reliefweb_tools = kernel.plugins["ReliefWebMCP"]

    for tool in reliefweb_tools:
        print(tool.name)
```

```bash
GetBlogs
GetDisasters
GetJobs
GetReports
GetResources
GetTrainings
```
