using System.Net.Http.Json;
using Livros.Data.Entities;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Livros.Tests.IntegrationTests;

public class WeatherForecastControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly HttpClient _client;

    public WeatherForecastControllerTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Get_ReturnsWeatherForecast()
    {
        var response = await _client.GetAsync("/weatherforecast");
        response.EnsureSuccessStatusCode();
        var forecasts = await response.Content.ReadFromJsonAsync<IEnumerable<WeatherForecast>>();
        Assert.NotNull(forecasts);
        Assert.NotEmpty(forecasts);
    }
}
