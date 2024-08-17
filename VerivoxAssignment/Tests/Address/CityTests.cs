using System.Net;
using System.Text.RegularExpressions;
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
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var responseModel = System.Text.Json.JsonSerializer.Deserialize<CitiesResponseModel>(response.Content);

            Assert.NotNull(responseModel?.Cities);
            Assert.NotEmpty(responseModel.Cities);

            var cities = responseModel.Cities;

            Assert.True(cities.Length == expectedCities.Length);
            foreach (var expectedCity in expectedCities)
            {
                Assert.Contains(expectedCity, cities);
            }
        }

        [Theory(Skip = "Extra test to validate german zipcodes")]
        [InlineData("00123", false)]
        [InlineData("1409", false)]
        [InlineData("104090", false)]
        [InlineData("10409", true)] //Berlin
        [InlineData("01067", true)] //Dresden
        public void ZipCode_ShouldAlwaysBeInValidRange_ForAllGermanZipCodes(string zipcode, bool isValid)
        {
            var validationResult = new Regex(@"^(?!00)\d{5}$").IsMatch(zipcode);
            if (validationResult != isValid)
                Assert.Fail("Valid german zipcodes are 01000-99999.");
        }
    }
}