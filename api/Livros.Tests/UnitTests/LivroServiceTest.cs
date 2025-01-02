using Livros.Application.Dtos;
using Livros.Application.Services;
using Livros.Data;
using Livros.Data.Entities;

namespace Livros.Tests.UnitTests;

public class LivroServiceTest : BaseTest
{
	private LivroService CreateService(LivrosContext context)
	{
		return new LivroService(context);
	}

	[Fact]
	public async Task CreateAsync_ShouldAddLivro()
	{
		var options = GetNewContextOptionsWithRandomInmemoryDatabase();
		using var context = new LivrosContext(options);
		var service = CreateService(context);

		var livroDto = new LivroDto
		{
			Titulo = "Test Livro",
			Editora = "Test Editora",
			Edicao = 1,
			AnoPublicacao = 2021,
			AssuntoCods = new List<int> { 1 },
			AutorCods = new List<int> { 1 }
		};



		var result = await service.CreateAsync(livroDto);

		Assert.NotNull(result);
		Assert.Equal("Test Livro", result.Titulo);
		Assert.Equal("Test Editora", result.Editora);
		Assert.Equal(1, result.Edicao);
		Assert.Equal(2021, result.AnoPublicacao);
	}

	[Fact]
	public async Task GetByCodAsync_ShouldReturnLivro()
	{
		var options = GetNewContextOptionsWithRandomInmemoryDatabase();
		using var context = new LivrosContext(options);
		var service = CreateService(context);

		var livro = new Livro
		{
			Titulo = "Test Livro",
			Editora = "Test Editora",
			Edicao = 1,
			AnoPublicacao = 2021
		};

		context.Livros.Add(livro);
		await context.SaveChangesAsync();

		var result = await service.GetByCodAsync(livro.Cod);

		Assert.NotNull(result);
		Assert.Equal("Test Livro", result.Titulo);
		Assert.Equal("Test Editora", result.Editora);
		Assert.Equal(1, result.Edicao);
		Assert.Equal(2021, result.AnoPublicacao);
	}

	[Fact]
	public async Task GetAllAsync_ShouldReturnAllLivros()
	{
		var options = GetNewContextOptionsWithRandomInmemoryDatabase();
		using var context = new LivrosContext(options);
		var service = CreateService(context);

		context.Livros.AddRange(
			new Livro { Titulo = "Livro 1", Editora = "Editora 1", Edicao = 1, AnoPublicacao = 2001 },
			new Livro { Titulo = "Livro 2", Editora = "Editora 2", Edicao = 2, AnoPublicacao = 2002 }
		);
		await context.SaveChangesAsync();

		var result = await service.GetAllAsync();

		Assert.Equal(2, result.Count);
		Assert.Contains(result, l => l.Titulo == "Livro 1");
		Assert.Contains(result, l => l.Titulo == "Livro 2");
	}

	[Fact]
	public async Task UpdateAsync_ShouldUpdateLivro()
	{
		var options = GetNewContextOptionsWithRandomInmemoryDatabase();
		using var context = new LivrosContext(options);
		var service = CreateService(context);

		var livro = new Livro
		{
			Titulo = "Test Livro",
			Editora = "Test Editora",
			Edicao = 1,
			AnoPublicacao = 2021
		};

		context.Livros.Add(livro);
		await context.SaveChangesAsync();

		var livroDto = new LivroDto
		{
			Cod = livro.Cod,
			Titulo = "Updated Livro",
			Editora = "Test Editora",
			Edicao = 1,
			AnoPublicacao = 2021,
			AssuntoCods = new List<int> { 1 },
			AutorCods = new List<int> { 1 }
		};

		var result = await service.UpdateAsync(livroDto);

		Assert.NotNull(result);
		Assert.Equal("Updated Livro", result.Titulo);
	}

	[Fact]
	public async Task DeleteAsync_ShouldRemoveLivro()
	{
		var options = GetNewContextOptionsWithRandomInmemoryDatabase();
		using var context = new LivrosContext(options);
		var service = CreateService(context);

		var livro = new Livro
		{
			Titulo = "Test Livro",
			Editora = "Test Editora",
			Edicao = 1,
			AnoPublicacao = 2021
		};

		context.Livros.Add(livro);
		await context.SaveChangesAsync();

		var result = await service.DeleteAsync(livro.Cod);

		Assert.True(result);
		Assert.Null(await context.Livros.FindAsync(livro.Cod));
	}

	[Fact]
	public void GetRandomLivros_ShouldReturnFiveRandomLivros()
	{
		var options = GetNewContextOptionsWithRandomInmemoryDatabase();
		using var context = new LivrosContext(options);
		var service = CreateService(context);

		var result = service.GetRandomLivros();

		Assert.Equal(5, result.Count());
		Assert.Contains(result, l => l.Titulo == "Livro 1" && l.Editora == "Editora 1" && l.Edicao == 1 && l.AnoPublicacao == 2001);
		Assert.Contains(result, l => l.Titulo == "Livro 2" && l.Editora == "Editora 2" && l.Edicao == 2 && l.AnoPublicacao == 2002);
		Assert.Contains(result, l => l.Titulo == "Livro 3" && l.Editora == "Editora 3" && l.Edicao == 3 && l.AnoPublicacao == 2003);
		Assert.Contains(result, l => l.Titulo == "Livro 4" && l.Editora == "Editora 4" && l.Edicao == 4 && l.AnoPublicacao == 2004);
		Assert.Contains(result, l => l.Titulo == "Livro 5" && l.Editora == "Editora 5" && l.Edicao == 5 && l.AnoPublicacao == 2005);
	}
}