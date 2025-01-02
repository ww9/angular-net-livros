using Livros.Data;
using Livros.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Livros.Application.Services;

public class LivroService : ILivroService
{
	private readonly LivrosContext _context;

	public LivroService(LivrosContext context)
	{
		_context = context;
	}

	public IEnumerable<Livro> GetRandomLivros()
	{
		return Enumerable.Range(1, 5).Select(index => new Livro
		{
			Titulo = $"Livro {index}",
			Editora = $"Editora {index}",
			Edicao = index,
			AnoPublicacao = 2000 + index
		}).ToArray();
	}

	public async Task<Livro> CreateAsync(Livro livro)
	{
		_context.Livros.Add(livro);
		await _context.SaveChangesAsync();
		return livro;
	}

	public async Task<Livro> GetByCodAsync(int cod)
	{
		return await _context.Livros
			 .Include(l => l.LivroAutores).ThenInclude(la => la.Autor)
			 .Include(l => l.LivroAssuntos).ThenInclude(las => las.Assunto)
			 .FirstOrDefaultAsync(l => l.Cod == cod);
	}

	public async Task<List<Livro>> GetAllAsync()
	{
		return await _context.Livros
			 .Include(l => l.LivroAutores).ThenInclude(la => la.Autor)
			 .Include(l => l.LivroAssuntos).ThenInclude(las => las.Assunto)
			 .ToListAsync();
	}

	public async Task<Livro> UpdateAsync(Livro livro)
	{
		_context.Livros.Update(livro);
		await _context.SaveChangesAsync();
		return livro;
	}

	public async Task<bool> DeleteAsync(int cod)
	{
		var livro = await _context.Livros.FindAsync(cod);
		if (livro == null) return false;

		_context.Livros.Remove(livro);
		await _context.SaveChangesAsync();
		return true;
	}

	public void SeedDatabase()
	{
		var autores = new List<Autor>
		{
			new Autor { Nome = "J. R. R. Tolkien" },
			new Autor { Nome = "Miguel de Cervantes Saavedra" }
		};

		var livros = new List<Livro>
		{
			new Livro { Titulo = "O Senhor dos Anéis", Editora = "Allen & Unwin", AnoPublicacao = 1954, Edicao = 1 },
			new Livro { Titulo = "Dom Quixote", Editora = "Livraria José Olympio Editora", AnoPublicacao = 1952, Edicao = 1 }
		};

		var assuntos = new List<Assunto>
		{
			new Assunto { Descricao = "Ficção" },
			new Assunto { Descricao = "Aventura" },
			new Assunto { Descricao = "Romance"},
			new Assunto { Descricao = "Auto ajuda" },
			new Assunto { Descricao = "Terror" },
			new Assunto { Descricao = "Fantasia" },
		};

		var formacompras = new List<FormaCompra>
		{
			new FormaCompra { Descricao = "Balcão" },
			new FormaCompra { Descricao = "Self-service" },
			new FormaCompra { Descricao = "Internet" },
			new FormaCompra { Descricao = "Evento" }
		};

		_context.Autores.AddRange(autores);
		_context.Livros.AddRange(livros);
		_context.Assuntos.AddRange(assuntos);
		_context.FormaCompras.AddRange(formacompras);
		_context.SaveChanges();

		var livroAutores = new List<LivroAutor>
		{
			new LivroAutor { LivroCod = livros[0].Cod, AutorCod = autores[0].Cod },
			new LivroAutor { LivroCod = livros[1].Cod, AutorCod = autores[1].Cod }
		};

		var livroAssuntos = new List<LivroAssunto>
		{
			new LivroAssunto { LivroCod = livros[0].Cod, AssuntoCod = assuntos[0].Cod },
			new LivroAssunto { LivroCod = livros[1].Cod, AssuntoCod = assuntos[1].Cod }
		};

		var livroFormaCompras = new List<LivroFormaCompra>
		{
			new LivroFormaCompra { LivroCod = livros[0].Cod, FormaCompraCod = formacompras[0].Cod, Valor = 10.0 },
			new LivroFormaCompra { LivroCod = livros[1].Cod, FormaCompraCod = formacompras[1].Cod, Valor = 20.0 }
		};

		_context.LivroAutores.AddRange(livroAutores);
		_context.LivroAssuntos.AddRange(livroAssuntos);
		_context.LivroFormaCompras.AddRange(livroFormaCompras);
		_context.SaveChanges();

		// Clear entities to avoid cyclic reference
		_context.ChangeTracker.Clear();
	}
}