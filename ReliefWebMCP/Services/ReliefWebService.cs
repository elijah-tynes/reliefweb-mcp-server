using System.Text.Json;

namespace ReliefWebMCP;

public class ReliefWebService
{
    // Determine if MAX_REQUEST_SIZE was set by the user
    private int? _maxRequestSize = int.TryParse(Environment.GetEnvironmentVariable("MAX_REQUEST_SIZE"), out var parsed) ? parsed : null;

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

        // Env max request size or default set by LLM
        int querySize = _maxRequestSize ?? numResults;

        try
        {
            // Get reports sorted by date
            var response = await _httpClient.GetAsync($"https://api.reliefweb.int/v1/reports?&query[fields][]=title&query[value]={queryString}&fields[include][]=url&fields[include][]=country.shortname&fields[include][]=country.iso3&fields[include][]=disaster.name&fields[include][]=disaster.type.name&fields[include][]=theme.name&fields[include][]=date.created&sort[]=date.created:desc&fields[include][]=source.name&fields[include][]=source.homepage&limit={querySize}");
            response.EnsureSuccessStatusCode();

            string responseBody = await response.Content.ReadAsStringAsync();

            // Fallback query if responseBody is empty
            if (IsEmptyResult(responseBody) && !queryString.Contains("*"))
            {
                queryString = "*";
                response = await _httpClient.GetAsync($"https://api.reliefweb.int/v1/reports?&query[fields][]=title&query[value]={queryString}&fields[include][]=url&fields[include][]=country.shortname&fields[include][]=country.iso3&fields[include][]=disaster.name&fields[include][]=disaster.type.name&fields[include][]=theme.name&fields[include][]=date.created&sort[]=date.created:desc&fields[include][]=source.name&fields[include][]=source.homepage&limit={querySize}");
                response.EnsureSuccessStatusCode();

                responseBody = await response.Content.ReadAsStringAsync();
            }

            return responseBody;
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

        // Env max request size or default set by LLM
        int querySize = _maxRequestSize ?? numResults;

        try
        {
            // Get disasters sorted by date
            var response = await _httpClient.GetAsync($"https://api.reliefweb.int/v1/disasters?&query[fields][]=name&query[value]={queryString}&fields[include][]=profile.appeals_response_plans.active&fields[include][]=profile.useful_links.active&fields[include][]=glide&fields[include][]=type.name&fields[include][]=country.shortname&fields[include][]=country.iso3&fields[include][]=date.event&sort[]=date.event:desc&fields[include][]=url&limit={querySize}");
            response.EnsureSuccessStatusCode();

            string responseBody = await response.Content.ReadAsStringAsync();

            // Fallback query if responseBody is empty
            if (IsEmptyResult(responseBody) && !queryString.Contains("*"))
            {
                queryString = "*";
                response = await _httpClient.GetAsync($"https://api.reliefweb.int/v1/disasters?&query[fields][]=name&query[value]={queryString}&fields[include][]=profile.appeals_response_plans.active&fields[include][]=profile.useful_links.active&fields[include][]=glide&fields[include][]=type.name&fields[include][]=country.shortname&fields[include][]=country.iso3&fields[include][]=date.event&sort[]=date.event:desc&fields[include][]=url&limit={querySize}");
                response.EnsureSuccessStatusCode();

                responseBody = await response.Content.ReadAsStringAsync();
            }

            return responseBody;
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

        // Env max request size or default set by LLM
        int querySize = _maxRequestSize ?? numResults;

        try
        {
            // Get jobs using specified keywords
            var response = await _httpClient.GetAsync($"https://api.reliefweb.int/v1/jobs?&query[fields][]=title&query[value]={queryString}&fields[include][]=country.shortname&fields[include][]=country.name&fields[include][]=city.name&fields[include][]=type.name&fields[include][]=career_categories.name&fields[include][]=body&fields[include][]=date.created&fields[include][]=date.closing&sort[]=date.created:desc&fields[include][]=url&fields[include][]=experience.name&fields[include][]=how_to_apply&limit={querySize}");
            response.EnsureSuccessStatusCode();

            string responseBody = await response.Content.ReadAsStringAsync();

            // Fallback query if responseBody is empty
            if (IsEmptyResult(responseBody) && !queryString.Contains("*"))
            {
                queryString = "*";
                response = await _httpClient.GetAsync($"https://api.reliefweb.int/v1/jobs?&query[fields][]=title&query[value]={queryString}&fields[include][]=country.shortname&fields[include][]=country.name&fields[include][]=city.name&fields[include][]=type.name&fields[include][]=career_categories.name&fields[include][]=body&fields[include][]=date.created&fields[include][]=date.closing&sort[]=date.created:desc&fields[include][]=url&fields[include][]=experience.name&fields[include][]=how_to_apply&limit={querySize}");
                response.EnsureSuccessStatusCode();

                responseBody = await response.Content.ReadAsStringAsync();
            }

            return responseBody;
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

        // Env max request size or default set by LLM
        int querySize = _maxRequestSize ?? numResults;

        try
        {
            // Get trainings using specified keywords
            var response = await _httpClient.GetAsync($"https://api.reliefweb.int/v1/training?&query[fields][]=title&query[value]={queryString}&fields[include][]=country.shortname&fields[include][]=country.iso3&fields[include][]=city.name&fields[include][]=career_categories.name&fields[include][]=cost&fields[include][]=date.created&sort[]=date.created:desc&fields[include][]=how_to_register&fields[include][]=url&fields[include][]=training_language.name&fields[include][]=training_language.code&fields[include][]=type.name&fields[include][]=source.name&fields[include][]=format.name&limit={querySize}");
            response.EnsureSuccessStatusCode();

            string responseBody = await response.Content.ReadAsStringAsync();

            // Fallback query if responseBody is empty
            if (IsEmptyResult(responseBody) && !queryString.Contains("*"))
            {
                queryString = "*";
                response = await _httpClient.GetAsync($"https://api.reliefweb.int/v1/training?&query[fields][]=title&query[value]={queryString}&fields[include][]=country.shortname&fields[include][]=country.iso3&fields[include][]=city.name&fields[include][]=career_categories.name&fields[include][]=cost&fields[include][]=date.created&sort[]=date.created:desc&fields[include][]=how_to_register&fields[include][]=url&fields[include][]=training_language.name&fields[include][]=training_language.code&fields[include][]=type.name&fields[include][]=source.name&fields[include][]=format.name&limit={querySize}");
                response.EnsureSuccessStatusCode();

                responseBody = await response.Content.ReadAsStringAsync();
            }

            return responseBody;
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

        // Env max request size or default set by LLM
        int querySize = _maxRequestSize ?? numResults;

        try
        {
            // Get blogs sorted by date using specified keywords
            var response = await _httpClient.GetAsync($"https://api.reliefweb.int/v1/blog?&query[fields][]=title&query[value]={queryString}&fields[include][]=author&fields[include][]=url&fields[include][]=tags.name&fields[include][]=date.created&sort[]=date.created:desc&limit={querySize}");
            response.EnsureSuccessStatusCode();

            string responseBody = await response.Content.ReadAsStringAsync();

            // Fallback query if responseBody is empty
            if (IsEmptyResult(responseBody) && !queryString.Contains("*"))
            {
                queryString = "*";
                response = await _httpClient.GetAsync($"https://api.reliefweb.int/v1/blog?&query[fields][]=title&query[value]={queryString}&fields[include][]=author&fields[include][]=url&fields[include][]=tags.name&fields[include][]=date.created&sort[]=date.created:desc&limit={querySize}");
                response.EnsureSuccessStatusCode();

                responseBody = await response.Content.ReadAsStringAsync();
            }

            return responseBody;
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

        // Env max request size or default set by LLM
        int querySize = _maxRequestSize ?? numResults;

        try
        {
            // Get resources using specified keywords
            var response = await _httpClient.GetAsync($"https://api.reliefweb.int/v1/book?&query[fields][]=title&query[value]={queryString}&fields[include][]=url&fields[include][]=date.created&sort[]=date.created:desc&limit={querySize}");
            response.EnsureSuccessStatusCode();

            string responseBody = await response.Content.ReadAsStringAsync();

            // Fallback query if responseBody is empty
            if (IsEmptyResult(responseBody) && !queryString.Contains("*"))
            {
                queryString = "*";
                response = await _httpClient.GetAsync($"https://api.reliefweb.int/v1/book?&query[fields][]=title&query[value]={queryString}&fields[include][]=url&fields[include][]=date.created&sort[]=date.created:desc&limit={querySize}");
                response.EnsureSuccessStatusCode();

                responseBody = await response.Content.ReadAsStringAsync();
            }

            // Return response
            return responseBody;
        }
        catch (Exception e)
        {
            return $"GetResources request error: {e.Message}";
        }
    }

    // Helper function to determine if a response has content
    private bool IsEmptyResult(string responseBody)
    {
        var json = JsonDocument.Parse(responseBody);
        return json.RootElement.TryGetProperty("data", out var dataProp) && dataProp.GetArrayLength() == 0;
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
}