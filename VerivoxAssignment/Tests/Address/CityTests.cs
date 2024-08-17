using System.Net;
using System.Text.RegularExpressions;
using RestSharp;
using VerivoxAssignment.Models;
using FluentAssertions;

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
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            response.Content.Should().BeNullOrEmpty();
        }

        [Theory]
        [InlineData("10409", new string[] { "Berlin" })]
        [InlineData("77716", new string[] { "Fischerbach", "Haslach", "Hofstetten" })]
        public async void GetCities_ShouldReturnExpectedCities_ForValidZipcode(string zipcode, string[] expectedCities)
        {
            // Arrange
            var request = new RestRequest($"/{zipcode}");

            // Act
            var response = await _client.ExecuteGetAsync(request);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var responseModel = System.Text.Json.JsonSerializer.Deserialize<CitiesResponseModel>(response.Content);

            responseModel.Should().NotBeNull();
            responseModel.Cities.Should().NotBeEmpty()
                .And.HaveCount(expectedCities.Length)
                .And.Contain(expectedCities);
        }

        [Theory(Skip = "Extra test to validate german zipcodes")]
        [InlineData("00123", false)]
        [InlineData("1409", false)]
        [InlineData("104090", false)]
        [InlineData("10409", true)] //Berlin
        [InlineData("01067", true)] //Dresden
        public void ZipCode_ShouldAlwaysBeInValidRange_ForAllGermanZipCodes(string zipcode, bool isValid)
        {
            // Act
            var validationResult = new Regex(@"^(?!00)\d{5}$").IsMatch(zipcode);

            // Assert
            validationResult.Should().Be(isValid, "valid German zip codes are in the range of 01000-99999.");
        }
    }
}