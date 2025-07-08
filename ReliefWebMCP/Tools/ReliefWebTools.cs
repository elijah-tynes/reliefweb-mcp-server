using System.ComponentModel;
using ModelContextProtocol.Server;

namespace ReliefWebMCP;

[McpServerToolType]
public class ReliefWebTools
{
    [McpServerTool, Description(
@"Retrieves update and situation reports curated from ReliefWeb.
If no keywords are provided, a list of reports sorted by date is provided.
Each result includes: Report title, country of report, iso3 code of the report, disaster related to the report (if applicable), theme of the report, date the report was created, source of the report, and ReliefWeb report URL for more info on the report.")]
    public async Task<string> GetReports(ReliefWebService reliefWebService, string[]? keywords = null, int numResults = 20)
    {
        var reports = await reliefWebService.GetReports(keywords, numResults);
        return reports;
    }

    [McpServerTool, Description(
@"Retrieves information related to disasters.
If no keywords are provided, a list of disasters sorted by date is provided.
Each result includes: Name of the incident, glide identifier of the incident, country of the incident, iso3 code of the incident, type of incident, date of the incident, and ReliefWeb URL for more info on the incident.")]
    public async Task<string> GetDisasters(ReliefWebService reliefWebService, string[]? keywords = null, int numResults = 20)
    {
        var disasters = await reliefWebService.GetDisasters(keywords, numResults);
        return disasters;
    }

    [McpServerTool, Description(
@"Retrieves ReliefWeb humanitarian jobs and volunteer opportunities.
If no keywords are provided, a list of jobs sorted by date is provided.
Each result includes: Title of the opportunity, country of the role, city of the role (if applicable), iso3 code of the role, type of role, career category of the role, date the role application opened, date the role appplication closes, required experience for the role, how to apply for the role, and the ReliefWeb URL for more information on the role.")]
    public async Task<string> GetJobs(ReliefWebService reliefWebService, [Description("When specifying a country, use 3-letter ISO 3166-1 alpha-3 code")] string[]? keywords = null, int numResults = 20)
    {
        var jobs = await reliefWebService.GetJobs(keywords, numResults);
        return jobs;
    }

    [McpServerTool, Description(
@"Retrieves training opportunities and courses for useful and necessary humanitarian skills.
If no keywords are provided, a list of trainings sorted by date is provided.
Each result includes: Title of training, country of the training, city of the training, iso3 code of the training, career category of the training, cost of the training, date the training was created, how to register for the training, source of the training, type of training, language of the training, format of the training, and ReliefWeb URL for more information on the training.")]
    public async Task<string> GetTrainings(ReliefWebService reliefWebService, string[]? keywords = null, int numResults = 20)
    {
        var trainings = await reliefWebService.GetTrainings(keywords, numResults);
        return trainings;
    }

    [McpServerTool, Description(
@"Retrieves blog posts about ideas to grow and improve ReliefWeb.
If no keywords are provided, a list of blogs sorted by date is provided.
Each result includes: Blog title, blog author, blog tags, date the blog was created, and the ReliefWeb blog URL for more info.")]
    public async Task<string> GetBlogs(ReliefWebService reliefWebService, string[]? keywords = null, int numResults = 20)
    {
        var blogs = await reliefWebService.GetBlogs(keywords, numResults);
        return blogs;
    }

    [McpServerTool, Description(
@"Retrieves static information about the ReliefWeb site and humanitarian resources.
There are pages containing content on: help, terms and conditions, location maps, taxonomy descriptions, and how to share humanitarian content on ReliefWeb.
If no keywords are provided, a list of resources sorted by date is provided.
Each result includes: Resource title, date the resource was created, and ReliefWeb resource URL for more info.")]
    public async Task<string> GetResources(ReliefWebService reliefWebService, string[]? keywords = null, int numResults = 20)
    {
        var resources = await reliefWebService.GetResources(keywords, numResults);
        return resources;
    }
}