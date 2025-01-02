using Livros.Application.Services;
using Livros.Data;
using Livros.Data.Entities;
using Livros.Tests.UnitTests;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Xunit;

namespace Livros.Tests.UnitTests;

public class AutorServiceTest : BaseTest
{
	private AutorService CreateService(LivrosContext context)
	{
		return new AutorService(context);
	}

	[Fact]
	public async Task CreateAsync_ShouldAddAutor()
	{
		var options = GetNewContextOptionsWithRandomInmemoryDatabase();
		using var context = new LivrosContext(options);
		var service = CreateService(context);

		var autor = new Autor
		{
			Nome = "Test Autor"
		};

		var result = await service.CreateAsync(autor);

		Assert.NotNull(result);
		Assert.Equal("Test Autor", result.Nome);
	}

	[Fact]
	public async Task GetByCodAsync_ShouldReturnAutor()
	{
		var options = GetNewContextOptionsWithRandomInmemoryDatabase();
		using var context = new LivrosContext(options);
		var service = CreateService(context);

		var autor = new Autor
		{
			Nome = "Test Autor"
		};

		context.Autores.Add(autor);
		await context.SaveChangesAsync();

		var result = await service.GetByCodAsync(autor.Cod);

		Assert.NotNull(result);
		Assert.Equal("Test Autor", result.Nome);
	}

	[Fact]
	public async Task GetAllAsync_ShouldReturnAllAutores()
	{
		var options = GetNewContextOptionsWithRandomInmemoryDatabase();
		using var context = new LivrosContext(options);
		var service = CreateService(context);

		context.Autores.AddRange(
			new Autor { Nome = "Autor 1" },
			new Autor { Nome = "Autor 2" }
		);
		await context.SaveChangesAsync();

		var result = await service.GetAllAsync();

		Assert.Equal(2, result.Count);
		Assert.Contains(result, a => a.Nome == "Autor 1");
		Assert.Contains(result, a => a.Nome == "Autor 2");
	}

	[Fact]
	public async Task UpdateAsync_ShouldUpdateAutor()
	{
		var options = GetNewContextOptionsWithRandomInmemoryDatabase();
		using var context = new LivrosContext(options);
		var service = CreateService(context);

		var autor = new Autor
		{
			Nome = "Test Autor"
		};

		context.Autores.Add(autor);
		await context.SaveChangesAsync();

		autor.Nome = "Updated Autor";
		var result = await service.UpdateAsync(autor);

		Assert.NotNull(result);
		Assert.Equal("Updated Autor", result.Nome);
	}

	[Fact]
	public async Task DeleteAsync_ShouldRemoveAutor()
	{
		var options = GetNewContextOptionsWithRandomInmemoryDatabase();
		using var context = new LivrosContext(options);
		var service = CreateService(context);

		var autor = new Autor
		{
			Nome = "Test Autor"
		};

		context.Autores.Add(autor);
		await context.SaveChangesAsync();

		var result = await service.DeleteAsync(autor.Cod);

		Assert.True(result);
		Assert.Null(await context.Autores.FindAsync(autor.Cod));
	}
}