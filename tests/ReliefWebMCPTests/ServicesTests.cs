using System.Reflection;
using Moq;
using ReliefWebMCP;

namespace ReliefWebMCPTests;

public class TestReliefWebService : ReliefWebService
{
    // Override ExecuteQuery to return a fake response for testing
    protected override async Task<string> ExecuteQuery(string endpoint)
    {
        string fakeJson = "{\"data\": [{\"title\": \"Mock Report\"}]}";
        return await Task.FromResult(fakeJson);
    }
}

public class ServicesTests
{
    // Service instance to be tested
    private TestReliefWebService _service;

    public ServicesTests()
    {
        _service = new TestReliefWebService();
    }

    [Fact]
    public async Task GetReportsTest()
    {
        // Test GetReports with query parameters
        // Arrange
        string[]? keywords = { "flood", "earthquake" };
        int numResults = 5;

        // Act
        string result = await _service.GetReports(keywords, numResults);

        // Assert
        Assert.Contains("Mock Report", result);

        // Test GetReports without query parameters
        result = await _service.GetReports(null, numResults);
        Assert.Contains("Mock Report", result);
    }

    [Fact]
    public async Task GetDisastersTest()
    {
        // Test GetDisasters with query parameters
        // Arrange
        string[] keywords = { "flood", "earthquake" };
        int numResults = 5;

        // Act
        string result = await _service.GetDisasters(keywords, numResults);

        // Assert
        Assert.False(string.IsNullOrWhiteSpace(result));
        Assert.Contains("Mock Report", result);

        // Test GetDisasters without query parameters
        result = await _service.GetDisasters(null, numResults);
        Assert.Contains("Mock Report", result);
    }

    [Fact]
    public async Task GetJobsTest()
    {
        // Test GetJobs with query parameters
        // Arrange
        string[] keywords = { "volunteer" };
        int numResults = 5;

        // Act
        string result = await _service.GetJobs(keywords, numResults);

        // Assert
        Assert.False(string.IsNullOrWhiteSpace(result));
        Assert.Contains("Mock Report", result);

        // Test GetJobs without query parameters
        result = await _service.GetJobs(null, numResults);
        Assert.Contains("Mock Report", result);
    }

    [Fact]
    public async Task GetTrainingsTest()
    {
        // Test GetTrainings with query parameters
        // Arrange
        string[] keywords = { "training" };
        int numResults = 5;

        // Act
        string result = await _service.GetTrainings(keywords, numResults);

        // Assert
        Assert.False(string.IsNullOrWhiteSpace(result));
        Assert.Contains("Mock Report", result);

        // Test GetTrainings without query parameters
        result = await _service.GetTrainings(null, numResults);
        Assert.Contains("Mock Report", result);
    }

    [Fact]
    public async Task GetBlogsTest()
    {
        // Test GetBlogs with query parameters
        // Arrange
        string[] keywords = { "blog" };
        int numResults = 5;

        // Act
        string result = await _service.GetBlogs(keywords, numResults);

        // Assert
        Assert.False(string.IsNullOrWhiteSpace(result));
        Assert.Contains("Mock Report", result);

        // Test GetBlogs without query parameters
        result = await _service.GetBlogs(null, numResults);
        Assert.Contains("Mock Report", result);
    }

    [Fact]
    public async Task GetResourcesTest()
    {
        // Test GetResources with query parameters
        // Arrange
        string[] keywords = { "help" };
        int numResults = 5;
        
        // Act
        string result = await _service.GetResources(keywords, numResults);

        // Assert
        Assert.False(string.IsNullOrWhiteSpace(result));
        Assert.Contains("Mock Report", result);

        // Test GetResources without query parameters
        result = await _service.GetResources(null, numResults);
        Assert.Contains("Mock Report", result);
    }
}