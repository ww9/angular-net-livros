using Livros.Data;
using Livros.Data.Entities;
using Livros.Tests.UnitTests;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

public class LivroAutorLinkServiceTest : BaseTest
{
	private readonly LivrosContext _context;
	private readonly LivroAutorLinkService _service;

	public LivroAutorLinkServiceTest()
	{
		var options = GetNewContextOptionsWithRandomInmemoryDatabase();
		_context = new LivrosContext(options);
		_service = new LivroAutorLinkService(_context);

		SeedDatabase();
	}

	private void SeedDatabase()
	{
		var livro = new Livro { Cod = 1, Titulo = "Livro 1", Editora = "Editora 1", AnoPublicacao = 2021 };
		var autor = new Autor { Cod = 1, Nome = "Autor 1" };

		_context.Livros.Add(livro);
		_context.Autores.Add(autor);
		_context.SaveChanges();
	}

	[Fact]
	public async Task LinkAutorToLivro_ShouldCreateLink_WhenNotAlreadyLinked()
	{
		await _service.LinkAutorToLivro(1, 1);

		var link = await _context.LivroAutores
			.FirstOrDefaultAsync(la => la.LivroCod == 1 && la.AutorCod == 1);

		Assert.NotNull(link);
	}

	[Fact]
	public async Task LinkAutorToLivro_ShouldNotCreateLink_WhenAlreadyLinked()
	{
		await _service.LinkAutorToLivro(1, 1);
		await _service.LinkAutorToLivro(1, 1);

		var links = await _context.LivroAutores
			.Where(la => la.LivroCod == 1 && la.AutorCod == 1)
			.ToListAsync();

		Assert.Single(links);
	}

	[Fact]
	public async Task UnlinkAutorFromLivro_ShouldRemoveLink_WhenLinkExists()
	{
		await _service.LinkAutorToLivro(1, 1);
		await _service.UnlinkAutorFromLivro(1, 1);

		var link = await _context.LivroAutores
			.FirstOrDefaultAsync(la => la.LivroCod == 1 && la.AutorCod == 1);

		Assert.Null(link);
	}

	[Fact]
	public async Task UnlinkAutorFromLivro_ShouldDoNothing_WhenLinkDoesNotExist()
	{
		await _service.UnlinkAutorFromLivro(1, 1);

		var link = await _context.LivroAutores
			.FirstOrDefaultAsync(la => la.LivroCod == 1 && la.AutorCod == 1);

		Assert.Null(link);
	}

	[Fact]
	public async Task ListAutoresDeLivro_ShouldReturnAllLinkedAutores()
	{
		await _service.LinkAutorToLivro(1, 1);

		var autores = await _service.ListAutoresDeLivro(1);

		Assert.Single(autores);
		Assert.Equal(1, autores.First().Cod);
	}

	[Fact]
	public async Task ListLivrosDeAutor_ShouldReturnAllLinkedLivros()
	{
		await _service.LinkAutorToLivro(1, 1);

		var livros = await _service.ListLivrosDeAutor(1);

		Assert.Single(livros);
		Assert.Equal(1, livros.First().Cod);
	}
}