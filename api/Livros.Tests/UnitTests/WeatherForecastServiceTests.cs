using Livros.Application.Services;

namespace Livros.Tests.UnitTests
{
    public class WeatherForecastServiceTests
    {
        private readonly IWeatherForecastService _weatherForecastService;

        public WeatherForecastServiceTests()
        {
            _weatherForecastService = new WeatherForecastService();
        }

        [Fact]
        public void GetWeatherForecasts_ShouldReturnFiveForecasts()
        {
            // Act
            var forecasts = _weatherForecastService.GetWeatherForecasts();

            // Assert
            Assert.NotNull(forecasts);
            Assert.Equal(5, forecasts.Count());
        }

        [Fact]
        public void GetWeatherForecasts_ShouldReturnValidForecasts()
        {
            // Act
            var forecasts = _weatherForecastService.GetWeatherForecasts();

            // Assert
            foreach (var forecast in forecasts)
            {
                Assert.InRange(forecast.TemperatureC, -20, 55);
                Assert.Contains(forecast.Summary, WeatherForecastService.Summaries);
            }
        }
    }
}