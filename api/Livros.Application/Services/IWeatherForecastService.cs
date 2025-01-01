using Livros.Data.Entities;

namespace Livros.Application.Services;

public interface IWeatherForecastService
{
	IEnumerable<WeatherForecast> GetWeatherForecasts();
}