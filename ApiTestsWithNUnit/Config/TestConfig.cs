using NUnit.Framework;

namespace ApiTestsWithNUnit.Config;

public class Config
{
    public string BaseUrl { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty; 
    public string Email { get; set; } = string.Empty;
    public string ApiToken { get; set; } = string.Empty;
    public string NasaBaseUrl { get; set; } = string.Empty;
}

public class TestConfig
{
    public static Config GetConfig()
    {
        // Check if it is running in the CI
        bool isCi = Environment.GetEnvironmentVariable("GITHUB_ACTIONS") == "true";

        if (isCi)   
        {
            return new Config
            {
                BaseUrl = Environment.GetEnvironmentVariable("BASE_URL") 
                          ?? throw new Exception("BASE_URL not found in the CI"),
                UserName = Environment.GetEnvironmentVariable("API_KEY") 
                           ?? throw new Exception("USER_NAME not found in the  CI"),
                LastName = Environment.GetEnvironmentVariable("LAST_NAME") 
                           ?? throw new Exception("LAST_NAME not found in the  CI"),
                Email = Environment.GetEnvironmentVariable("LAST_NAME") 
                           ?? throw new Exception("EMAIL not found in the  CI"),
                ApiToken = Environment.GetEnvironmentVariable("API_TOKEN") 
                           ?? throw new Exception("API_TOKEN not found in the  CI"),
                NasaBaseUrl = Environment.GetEnvironmentVariable("NASA_BASE_URL") 
                           ?? throw new Exception("NASA_BASE_URL not found in the  CI")
            };
        }
        return new Config
        {
            BaseUrl = TestContext.Parameters["BaseUrl"] ?? "",
            UserName = TestContext.Parameters["UserName"] ?? "",
            LastName = TestContext.Parameters["LastName"] ?? "",
            Email = TestContext.Parameters["Email"] ?? "",
            ApiToken = TestContext.Parameters["ApiToken"] ?? "",
            NasaBaseUrl = TestContext.Parameters["NasaBaseUrl"] ?? ""
        };
    }
}