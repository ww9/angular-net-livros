using Livros.Application.Services;
using Livros.Data;
using Livros.Data.Entities;
using Livros.Tests.UnitTests;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
public class LivroAssuntoLinkServiceTest : BaseTest
{
	private readonly LivrosContext _context;
	private readonly LivroAssuntoLinkService _service;

	public LivroAssuntoLinkServiceTest()
	{
		var options = GetNewContextOptionsWithRandomInmemoryDatabase();
		_context = new LivrosContext(options);
		_service = new LivroAssuntoLinkService(_context);

		SeedDatabase();
	}

	private void SeedDatabase()
	{
		var livro = new Livro { Cod = 1, Titulo = "Test Livro", Editora = "Test Editora", AnoPublicacao = 2021 };
		var assunto1 = new Assunto { Cod = 1, Descricao = "Assunto 1" };
		var assunto2 = new Assunto { Cod = 2, Descricao = "Assunto 2" };

		_context.Livros.Add(livro);
		_context.Assuntos.AddRange(assunto1, assunto2);
		_context.SaveChanges();
	}

	[Fact]
	public async Task LinkAssuntoToLivro_ShouldCreateLink()
	{
		await _service.LinkAssuntoToLivro(1, 1);

		var link = await _context.LivroAssuntos.FirstOrDefaultAsync(la => la.LivroCod == 1 && la.AssuntoCod == 1);

		Assert.NotNull(link);
		Assert.Equal(1, link.LivroCod);
		Assert.Equal(1, link.AssuntoCod);
	}

	[Fact]
	public async Task UnlinkAssuntoFromLivro_ShouldRemoveLink()
	{
		await _service.LinkAssuntoToLivro(1, 1);
		await _service.UnlinkAssuntoFromLivro(1, 1);

		var removedLink = await _context.LivroAssuntos.FirstOrDefaultAsync(la => la.LivroCod == 1 && la.AssuntoCod == 1);

		Assert.Null(removedLink);
	}

	[Fact]
	public async Task ListAssuntosDeLivro_ShouldReturnAllLinkedAssuntos()
	{
		await _service.LinkAssuntoToLivro(1, 1);
		await _service.LinkAssuntoToLivro(1, 2);

		var result = await _service.ListAssuntosDeLivro(1);

		Assert.Equal(2, result.Count);
		Assert.Contains(result, a => a.Descricao == "Assunto 1");
		Assert.Contains(result, a => a.Descricao == "Assunto 2");
	}

	[Fact]
	public async Task ListLivrosDeAssunto_ShouldReturnAllLinkedLivros()
	{
		var livro1 = new Livro { Cod = 2, Titulo = "Livro 1", Editora = "Test Editora", AnoPublicacao = 2021 };
		var livro2 = new Livro { Cod = 3, Titulo = "Livro 2", Editora = "Test Editora", AnoPublicacao = 2021 };

		_context.Livros.AddRange(livro1, livro2);
		await _context.SaveChangesAsync();

		await _service.LinkAssuntoToLivro(2, 1);
		await _service.LinkAssuntoToLivro(3, 1);

		var result = await _service.ListLivrosDeAssunto(1);

		Assert.Equal(2, result.Count);
		Assert.Contains(result, l => l.Titulo == "Livro 1");
		Assert.Contains(result, l => l.Titulo == "Livro 2");
	}
}