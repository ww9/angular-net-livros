using Livros.Application.Services;

namespace Livros.Tests.UnitTests
{
    public class LivroServiceTests
    {
        private readonly ILivroService _livroService;

        public LivroServiceTests()
        {
            _livroService = new LivroService();
        }

        [Fact]
        public void GetLivro_ShouldReturnFiveForecasts()
        {
            // Act
            var forecasts = _livroService.GetLivro();

            // Assert
            Assert.NotNull(forecasts);
            Assert.Equal(5, forecasts.Count());
        }

        [Fact]
        public void GetLivro_ShouldReturnValidForecasts()
        {
            // Act
            var forecasts = _livroService.GetLivro();

            // Assert
            foreach (var forecast in forecasts)
            {
                Assert.InRange(forecast.TemperatureC, -20, 55);
                Assert.Contains(forecast.Summary, LivroService.Summaries);
            }
        }
    }
}