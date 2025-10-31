using System.Text.Json;

namespace ReliefWebMCP;

public class ReliefWebService
{
    // Extract optional request size environment variables for each MCP tool
    private int? _reportsRequestSize = GetRequestSize("REPORTS_REQUEST_SIZE");
    private int? _disastersRequestSize = GetRequestSize("DISASTERS_REQUEST_SIZE");
    private int? _jobsRequestSize = GetRequestSize("JOBS_REQUEST_SIZE");
    private int? _trainingsRequestSize = GetRequestSize("TRAININGS_REQUEST_SIZE");
    private int? _blogsRequestSize = GetRequestSize("BLOGS_REQUEST_SIZE");
    private int? _resourcesRequestSize = GetRequestSize("RESOURCES_REQUEST_SIZE");

    // HTTP client
    private HttpClient _httpClient;

    public ReliefWebService()
    {
        // Creates client
        _httpClient = new HttpClient();
    }

    // Retrieve reports curated from ReliefWeb
    public async Task<string> GetReports(string[]? keywords, int numResults)
    {
        // Extract and format keywords, set to "*" if none provided
        string queryString = BuildQueryString(keywords);

        // Reports request size or default
        int querySize = _reportsRequestSize ?? numResults;

        // String builder for the reports endpoint
        string BuildReportsEndpoint(string query) =>
            $"https://api.reliefweb.int/v2/reports?" +
            $"&appname=ReliefWebMCP" +
            $"&query[fields][]=title" +
            $"&query[value]={query}" +
            $"&fields[include][]=url" +
            $"&fields[include][]=country.shortname" +
            $"&fields[include][]=country.iso3" +
            $"&fields[include][]=disaster.name" +
            $"&fields[include][]=disaster.type.name" +
            $"&fields[include][]=theme.name" +
            $"&fields[include][]=date.created" +
            $"&sort[]=date.created:desc" +
            $"&fields[include][]=source.name" +
            $"&fields[include][]=source.homepage" +
            $"&limit={querySize}";

        try
        {
            // Build the target endpoint for 'Reports' using the provided keywords
            string reportsEndpoint = BuildReportsEndpoint(queryString);

            // Execute query on the reports endpoint
            string reports = await ExecuteQuery(reportsEndpoint);

            // Fallback case if response body is empty for a keyword query
            if (IsEmptyResult(reports) && !queryString.Contains("*"))
            {
                reports = await ExecuteQuery(BuildReportsEndpoint("*"));
            }

            return reports;
        }
        catch (Exception e)
        {
            return $"GetReports request error: {e.Message}";
        }
    }

    // Retrieve metadata on disasters
    public async Task<string> GetDisasters(string[]? keywords, int numResults)
    {
        // Extract and format keywords, set to "*" if none provided
        string queryString = BuildQueryString(keywords);

        // Disasters request size or default
        int querySize = _disastersRequestSize ?? numResults;

        // String builder for 'Disasters' endpoint
        string BuildDisastersEndpoint(string query) =>
            $"https://api.reliefweb.int/v2/disasters?" +
            $"&appname=ReliefWebMCP" +
            $"&query[fields][]=name" +
            $"&query[value]={query}" +
            $"&fields[include][]=profile.appeals_response_plans.active" +
            $"&fields[include][]=profile.useful_links.active" +
            $"&fields[include][]=glide" +
            $"&fields[include][]=type.name" +
            $"&fields[include][]=country.shortname" +
            $"&fields[include][]=country.iso3" +
            $"&fields[include][]=date.event" +
            $"&sort[]=date.event:desc" +
            $"&fields[include][]=url" +
            $"&limit={querySize}";

        try
        {
            // Build the target endpoint for 'Disasters' using the provided keywords
            string disastersEndpoint = BuildDisastersEndpoint(queryString);

            // Execute query on the disasters endpoint
            string disasters = await ExecuteQuery(disastersEndpoint);

            // Fallback case if response body is empty for a keyword query
            if (IsEmptyResult(disasters) && !queryString.Contains("*"))
            {
                disasters = await ExecuteQuery(BuildDisastersEndpoint("*"));
            }

            return disasters;
        }
        catch (Exception e)
        {
            return $"GetDisasters request error: {e.Message}";
        }
    }

    // Retrieve humanitarian jobs and volunteer opportunities
    public async Task<string> GetJobs(string[]? keywords, int numResults)
    {
        // Extract and format keywords, set to "*" if none provided
        string queryString = BuildQueryString(keywords);

        // Jobs request size or default 
        int querySize = _jobsRequestSize ?? numResults;

        // String builder for 'Jobs' endpoint
        string BuildJobsEndpoint(string query) =>
            $"https://api.reliefweb.int/v2/jobs?" +
            $"&appname=ReliefWebMCP" +
            $"&query[fields][]=title" +
            $"&query[value]={query}" +
            $"&fields[include][]=country.shortname" +
            $"&fields[include][]=country.name" +
            $"&fields[include][]=city.name" +
            $"&fields[include][]=type.name" +
            $"&fields[include][]=career_categories.name" +
            $"&fields[include][]=date.created" +
            $"&fields[include][]=date.closing" +
            $"&sort[]=date.created:desc" +
            $"&fields[include][]=url" +
            $"&fields[include][]=experience.name" +
            $"&fields[include][]=how_to_apply" +
            $"&limit={querySize}";

        try
        {
            // Build the target endpoint for 'Jobs' using the provided keywords
            string jobsEndpoint = BuildJobsEndpoint(queryString);

            // Execute query on the jobs endpoint
            string jobs = await ExecuteQuery(jobsEndpoint);

            // Fallback case if response body is empty for a keyword query
            if (IsEmptyResult(jobs) && !queryString.Contains("*"))
            {
                jobs = await ExecuteQuery(BuildJobsEndpoint("*"));
            }

            return jobs;
        }
        catch (Exception e)
        {
            return $"GetJobs request error: {e.Message}";
        }
    }

    // Retrieve training opportunities and courses for useful and necessary humanitarian skills
    public async Task<string> GetTrainings(string[]? keywords, int numResults)
    {
        // Extract and format keywords, set to "*" if none provided
        string queryString = BuildQueryString(keywords);

        // Trainings request size or default
        int querySize = _trainingsRequestSize ?? numResults;

        // String builder for 'Training' endpoint
        string BuildTrainingEndpoint(string query) =>
            $"https://api.reliefweb.int/v2/training?" +
            $"&appname=ReliefWebMCP" +
            $"&query[fields][]=title" +
            $"&query[value]={query}" +
            $"&fields[include][]=country.shortname" +
            $"&fields[include][]=country.iso3" +
            $"&fields[include][]=city.name" +
            $"&fields[include][]=career_categories.name" +
            $"&fields[include][]=cost" +
            $"&fields[include][]=date.created" +
            $"&sort[]=date.created:desc" +
            $"&fields[include][]=how_to_register" +
            $"&fields[include][]=url" +
            $"&fields[include][]=training_language.name" +
            $"&fields[include][]=training_language.code" +
            $"&fields[include][]=type.name" +
            $"&fields[include][]=source.name" +
            $"&fields[include][]=format.name" +
            $"&limit={querySize}";

        try
        {
            // Build the target endpoint for 'Training' using the provided keywords
            string trainingEndpoint = BuildTrainingEndpoint(queryString);

            // Execute query on the training endpoint
            string trainings = await ExecuteQuery(trainingEndpoint);

            // Fallback case if response body is empty for a keyword query
            if (IsEmptyResult(trainings) && !queryString.Contains("*"))
            {
                trainings = await ExecuteQuery(BuildTrainingEndpoint("*"));
            }

            return trainings;
        }
        catch (Exception e)
        {
            return $"GetTrainings request error: {e.Message}";
        }
    }

    // Retrieve blog posts about ideas to grow and improve ReliefWeb
    public async Task<string> GetBlogs(string[]? keywords, int numResults)
    {
        // Extract and format keywords, set to "*" if none provided
        string queryString = BuildQueryString(keywords);

        // Blogs request size or default 
        int querySize = _blogsRequestSize ?? numResults;

        // String builder for 'Blog' endpoint
        string BuildBlogsEndpoint(string query) =>
            $"https://api.reliefweb.int/v2/blog?" +
            $"&appname=ReliefWebMCP" +
            $"&query[fields][]=title" +
            $"&query[value]={query}" +
            $"&fields[include][]=author" +
            $"&fields[include][]=url" +
            $"&fields[include][]=tags.name" +
            $"&fields[include][]=date.created" +
            $"&sort[]=date.created:desc" +
            $"&limit={querySize}";

        try
        {
            // Build the target endpoint for 'Blog' using the provided keywords
            string blogsEndpoint = BuildBlogsEndpoint(queryString);

            // Execute query on the blog endpoint
            string blogs = await ExecuteQuery(blogsEndpoint);

            // Fallback case if response body is empty for a keyword query
            if (IsEmptyResult(blogs) && !queryString.Contains("*"))
            {
                blogs = await ExecuteQuery(BuildBlogsEndpoint("*"));
            }

            return blogs;
        }
        catch (Exception e)
        {
            return $"GetBlogs request error: {e.Message}";
        }
    }

    // Retrieve static information about ReliefWeb site and humanitarian resources
    public async Task<string> GetResources(string[]? keywords, int numResults)
    {
        // Extract and format keywords, set to "*" if none provided
        string queryString = BuildQueryString(keywords);

        // Resources request size or default 
        int querySize = _resourcesRequestSize ?? numResults;

        // String builder for 'Book' (resources) endpoint
        string BuildResourcesEndpoint(string query) =>
            $"https://api.reliefweb.int/v2/book?" +
            $"&appname=ReliefWebMCP" +
            $"&query[fields][]=title" +
            $"&query[value]={query}" +
            $"&fields[include][]=url" +
            $"&fields[include][]=date.created" +
            $"&sort[]=date.created:desc" +
            $"&limit={querySize}";

        try
        {
            // Build the target endpoint for resources using the provided keywords
            string resourcesEndpoint = BuildResourcesEndpoint(queryString);

            // Execute query on the endpoint to obtain resources
            string resources = await ExecuteQuery(resourcesEndpoint);

            // Fallback case if response body is empty for a keyword query
            if (IsEmptyResult(resources) && !queryString.Contains("*"))
            {
                resources = await ExecuteQuery(BuildResourcesEndpoint("*"));
            }

            return resources;
        }
        catch (Exception e)
        {
            return $"GetResources request error: {e.Message}";
        }
    }

    // Helper function to extract a request size for a given environment variable
    private static int? GetRequestSize(string envVarName)
    {
        return int.TryParse(Environment.GetEnvironmentVariable(envVarName), out var parsed) ? parsed : null;
    }

    // Helper function to build a keyword string for API queries
    private string BuildQueryString(string[]? keywords)
    {
        string? queryString;

        if (keywords == null || keywords.Length == 0 || keywords.Contains("*"))
        {
            // Default to * if no keywords provided
            queryString = "*";
        }
        else
        {
            // Combine keywords with '+' for search query 
            queryString = Uri.EscapeDataString(string.Join("+", keywords));
        }

        return queryString;
    }

    // Helper function to execute a ReliefWeb API query on a specified endpoint
    protected virtual async Task<string> ExecuteQuery(string endpoint)
    {
        // Send GET request to specified endpoint and ensure success code
        var response = await _httpClient.GetAsync(endpoint);
        response.EnsureSuccessStatusCode();

        // Read response as a string and return
        string responseBody = await response.Content.ReadAsStringAsync();
        return responseBody;
    }

    // Helper function to determine if a response has content
    private bool IsEmptyResult(string responseBody)
    {
        var json = JsonDocument.Parse(responseBody);
        return json.RootElement.TryGetProperty("data", out var dataProp) && dataProp.GetArrayLength() == 0;
    }
}