using Livros.Application.Services;
using Livros.Data;
using Livros.Data.Entities;
using System.Threading.Tasks;
using Xunit;

namespace Livros.Tests.UnitTests;

public class AssuntoServiceTest : BaseTest
{
	private AssuntoService CreateService(LivrosContext context)
	{
		return new AssuntoService(context);
	}

	[Fact]
	public async Task CreateAsync_ShouldAddAssunto()
	{
		var options = GetNewContextOptionsWithRandomInmemoryDatabase();
		using var context = new LivrosContext(options);
		var service = CreateService(context);

		var assunto = new Assunto
		{
			Descricao = "Test Assunto"
		};

		var result = await service.CreateAsync(assunto);

		Assert.NotNull(result);
		Assert.Equal("Test Assunto", result.Descricao);
	}

	[Fact]
	public async Task GetByCodAsync_ShouldReturnAssunto()
	{
		var options = GetNewContextOptionsWithRandomInmemoryDatabase();
		using var context = new LivrosContext(options);
		var service = CreateService(context);

		var assunto = new Assunto
		{
			Descricao = "Test Assunto"
		};

		context.Assuntos.Add(assunto);
		await context.SaveChangesAsync();

		var result = await service.GetByCodAsync(assunto.Cod);

		Assert.NotNull(result);
		Assert.Equal("Test Assunto", result.Descricao);
	}

	[Fact]
	public async Task GetAllAsync_ShouldReturnAllAssuntos()
	{
		var options = GetNewContextOptionsWithRandomInmemoryDatabase();
		using var context = new LivrosContext(options);
		var service = CreateService(context);

		context.Assuntos.AddRange(
			new Assunto { Descricao = "Assunto 1" },
			new Assunto { Descricao = "Assunto 2" }
		);
		await context.SaveChangesAsync();

		var result = await service.GetAllAsync();

		Assert.Equal(2, result.Count);
		Assert.Contains(result, a => a.Descricao == "Assunto 1");
		Assert.Contains(result, a => a.Descricao == "Assunto 2");
	}

	[Fact]
	public async Task UpdateAsync_ShouldUpdateAssunto()
	{
		var options = GetNewContextOptionsWithRandomInmemoryDatabase();
		using var context = new LivrosContext(options);
		var service = CreateService(context);

		var assunto = new Assunto
		{
			Descricao = "Test Assunto"
		};

		context.Assuntos.Add(assunto);
		await context.SaveChangesAsync();

		assunto.Descricao = "Updated Assunto";
		var result = await service.UpdateAsync(assunto);

		Assert.NotNull(result);
		Assert.Equal("Updated Assunto", result.Descricao);
	}

	[Fact]
	public async Task DeleteAsync_ShouldRemoveAssunto()
	{
		var options = GetNewContextOptionsWithRandomInmemoryDatabase();
		using var context = new LivrosContext(options);
		var service = CreateService(context);

		var assunto = new Assunto
		{
			Descricao = "Test Assunto"
		};

		context.Assuntos.Add(assunto);
		await context.SaveChangesAsync();

		var result = await service.DeleteAsync(assunto.Cod);

		Assert.True(result);
		Assert.Null(await context.Assuntos.FindAsync(assunto.Cod));
	}
}