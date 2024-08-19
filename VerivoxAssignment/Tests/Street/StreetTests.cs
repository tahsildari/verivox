using System.Net;
using RestSharp;
using VerivoxAssignment.Models;
using FluentAssertions;
using System.Diagnostics;

namespace VerivoxAssignment.Tests.Street;

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
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var responseModel = System.Text.Json.JsonSerializer.Deserialize<StreetsResponseModel>(response.Content);

        responseModel.Should().NotBeNull();
        responseModel.Streets.Should().NotBeEmpty()
            .And.HaveCount(streetsCount)
            .And.Contain(sampleStreets);
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
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async void GetStreets_ShouldReturnResponseWithinExpectedTime()
    {
        // Arrange
        var request = new RestRequest($"/10409/Berlin/streets");
        var expectedMaxResponseTimeMilliseconds = 200; 
        
        // Act
        var stopwatch = Stopwatch.StartNew();
        var response = await _client.ExecuteGetAsync(request);
        stopwatch.Stop(); 

        // Assert
        stopwatch.ElapsedMilliseconds.Should().BeLessThanOrEqualTo(expectedMaxResponseTimeMilliseconds,
            $"API call took too long: {stopwatch.ElapsedMilliseconds} ms. Expected under {expectedMaxResponseTimeMilliseconds} ms.");
    }
}