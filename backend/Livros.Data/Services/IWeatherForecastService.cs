using Livros.Data.Entities;

namespace Livros.Data.Services;

public interface IWeatherForecastService
{
	IEnumerable<WeatherForecast> GetWeatherForecasts();
}