using System.Net.NetworkInformation;
using Microsoft.Extensions.Configuration;
using RestSharp;

namespace VerivoxAssignment.Tests
{
    public class ApiTestBase
    {
        protected readonly RestClient _client;
        private readonly IConfiguration _configuration;
        private readonly string _apiUrl;

        public ApiTestBase()
        {
            _configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

            _apiUrl = _configuration["Settings:ApiUrl"] ?? string.Empty;
        
            _client = new RestClient(_apiUrl); // Replace with your API base URL
        }

        [Fact]
        private void CheckApiUrl()
        {
            Assert.False(string.IsNullOrEmpty(_apiUrl), "The API URL should not be null or empty.");
        }
    }
}