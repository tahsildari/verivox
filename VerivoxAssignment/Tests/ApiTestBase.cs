using System.Configuration;
using Microsoft.Extensions.Configuration;
using RestSharp;

namespace VerivoxAssignment.Tests;

public class ApiTestBase
{
    protected readonly RestClient _client;

    public ApiTestBase()
    {
        var configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json")
        .Build();

        var apiUrl = configuration["Settings:ApiUrl"];

        if (string.IsNullOrEmpty(apiUrl))
            throw new ConfigurationErrorsException("The API URL should not be null or empty.");
        
        _client = new RestClient(apiUrl);
    }
}