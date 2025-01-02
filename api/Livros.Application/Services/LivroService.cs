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
				new Autor { Nome = "Autor 1" },
				new Autor { Nome = "Autor 2" }
		  };

		var livros = new List<Livro>
		  {
				new Livro { Titulo = "Livro 1", Editora = "Editora 1", AnoPublicacao = 2021, Edicao = 1 },
				new Livro { Titulo = "Livro 2", Editora = "Editora 2", AnoPublicacao = 2022, Edicao = 1 }
		  };

		var assuntos = new List<Assunto>
		  {
				new Assunto { Descricao = "Assunto 1" },
				new Assunto { Descricao = "Assunto 2" }
		  };

		_context.Autores.AddRange(autores);
		_context.Livros.AddRange(livros);
		_context.Assuntos.AddRange(assuntos);
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

		_context.LivroAutores.AddRange(livroAutores);
		_context.LivroAssuntos.AddRange(livroAssuntos);
		_context.SaveChanges();

		// Clear entities to avoid cyclic reference
		_context.ChangeTracker.Clear();
	}
}