using Livros.Data.Entities;

namespace Livros.Application.Services;

public class LivroService : ILivroService
{
	public static readonly string[] Summaries = ["Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"];

	public IEnumerable<Livro> GetLivro()
	{
		return Enumerable.Range(1, 5).Select(index => new Livro
		{
			Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
			TemperatureC = Random.Shared.Next(-20, 55),
			Summary = Summaries[Random.Shared.Next(Summaries.Length)]
		}).ToArray();
	}
}