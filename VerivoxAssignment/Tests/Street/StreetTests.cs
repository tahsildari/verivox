using System.Net;
using RestSharp;
using VerivoxAssignment.Models;

namespace VerivoxAssignment.Tests.Street
{
    public class StreetTests : ApiTestBase
    {
        [Theory]
        [InlineData("10409", "Berlin", 29, new string[] { "Küselstr.", "Preußstr.", "Erich-Weinert-Str." })]
        [InlineData("77716", "Fischerbach", 34, new string[] { "Am Höfle", "Fritz-Ullmann-Weg" })]
        [InlineData("77716", "Haslach", 123, new string[] { "Dr.-Kempf-Str.", "Wolfgäßle" })]
        [InlineData("77716", "Hofstetten", 40, new string[] { "Weißer Brunnen" })]
        public async void GetStreets_ShouldReturnExpectedStreets_ForValidZipcodeAndCity(string zipcode, string city, int streetsCount, string[] sampleStreets)
        {
            // Arrange
            var request = new RestRequest($"/{zipcode}/{city}/streets");

            // Act
            var response = await _client.ExecuteGetAsync(request);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var responseModel = System.Text.Json.JsonSerializer.Deserialize<StreetsResponseModel>(response.Content);

            Assert.NotNull(responseModel?.Streets);
            Assert.NotEmpty(responseModel.Streets);

            var streets = responseModel.Streets;

            Assert.True(streets.Length == streetsCount);
            foreach (var expectedStreet in sampleStreets)
            {
                Assert.Contains(expectedStreet, streets);
            }
        }

        [Theory]
        [InlineData("10409", "Hofstetten")]
        public async void GetStreets_ShouldReturn404StatusCode_ForInvalidZipcodeAndCityPair(string zipcode, string city)
        {
            // Arrange
            var request = new RestRequest($"/{zipcode}/{city}/streets");

            // Act
            var response = await _client.ExecuteGetAsync(request);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}