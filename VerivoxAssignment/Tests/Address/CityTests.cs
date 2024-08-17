using System.Net;
using RestSharp;
using VerivoxAssignment.Models;

namespace VerivoxAssignment.Tests.Address
{
    public class CityTests : ApiTestBase
    {
        [Fact]
        public async void GetCities_ShouldReturn404_ForInvalidZipcode()
        {
            // Arrange
            var invalidZipcode = 22333;
            var request = new RestRequest($"/{invalidZipcode}");

            // Act
            var response = await _client.ExecuteGetAsync(request);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            Assert.Empty(response.Content);
        }

        [Fact]
        public async void GetCities_ShouldReturnBerlinOnly_ForZipcode10409()
        {
            // Arrange
            var zipcode = 10409;
            var request = new RestRequest($"/{zipcode}");

            // Act
            var response = await _client.ExecuteGetAsync(request);

            // Assert
            var responseModel = System.Text.Json.JsonSerializer.Deserialize<CitiesResponseModel>(response.Content);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var cities = responseModel.Cities;
            Assert.NotNull(responseModel?.Cities);
            Assert.NotEmpty(responseModel.Cities);

            Assert.Single(cities);
            Assert.Equal("Berlin", cities[0]);
        }

        [Fact]
        public async void GetCities_ShouldReturn3Cities_ForZipcode77716()
        {
            // Arrange
            var zipcode = 77716;
            var request = new RestRequest($"/{zipcode}");

            // Act
            var response = await _client.ExecuteGetAsync(request);

            var responseModel = System.Text.Json.JsonSerializer.Deserialize<CitiesResponseModel>(response.Content);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            Assert.NotNull(responseModel?.Cities);
            Assert.NotEmpty(responseModel.Cities);

            var cities = responseModel.Cities;

            // Assert
            Assert.True(cities.Length == 3);
            Assert.Contains("Fischerbach", cities);
            Assert.Contains("Haslach", cities);
            Assert.Contains("Hofstetten", cities);
        }
    }
}