using Livros.Application.Dtos;
using Livros.Application.Errors;
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

	public IEnumerable<LivroDto> GetRandomLivros()
	{
		return Enumerable.Range(1, 5).Select(index => new LivroDto
		{
			Titulo = $"Livro {index}",
			Editora = $"Editora {index}",
			Edicao = index,
			AnoPublicacao = 2000 + index
		});
	}

	public async Task<LivroDto> CreateAsync(LivroDto dto)
	{
		if (dto.Titulo.Length < 3 || dto.Titulo.Length > 40)
		{
			throw new ValidationException("Título deve ter entre 3 e 40 caracteres");
		}
		if (dto.AssuntoCods == null || !dto.AssuntoCods.Any())
		{
			throw new ValidationException("Ao menos um assunto deve ser selecionado");
		}
		if (dto.AutorCods == null || !dto.AutorCods.Any())
		{
			throw new ValidationException("Ao menos um autor deve ser selecionado");
		}
		var livro = new Livro
		{
			Titulo = dto.Titulo,
			Editora = dto.Editora,
			Edicao = dto.Edicao,
			AnoPublicacao = dto.AnoPublicacao
		};
		_context.Livros.Add(livro);
		await _context.SaveChangesAsync();

		// Link Assuntos
		await UpdateLivroAssuntosAsync(livro, dto.AssuntoCods);
		await UpdateLivroAutoresAsync(livro, dto.AutorCods);
		return new LivroDto
		{
			Cod = livro.Cod,
			Titulo = livro.Titulo,
			Editora = livro.Editora,
			Edicao = livro.Edicao,
			AnoPublicacao = livro.AnoPublicacao,
			AssuntoCods = dto.AssuntoCods
		};
	}

	public async Task<LivroDto> UpdateAsync(LivroDto dto)
	{
		if (dto.Titulo.Length < 3 || dto.Titulo.Length > 40)
		{
			throw new ValidationException("Título deve ter entre 3 e 40 caracteres");
		}
		if (dto.AssuntoCods == null || !dto.AssuntoCods.Any())
		{
			throw new ValidationException("Ao menos um assunto deve ser selecionado");
		}
		if (dto.AutorCods == null || !dto.AutorCods.Any())
		{
			throw new ValidationException("Ao menos um autor deve ser selecionado");
		}
		var livro = await _context.Livros.FirstOrDefaultAsync(l => l.Cod == dto.Cod);
		if (livro == null) throw new Exception("Livro not found.");

		livro.Titulo = dto.Titulo;
		livro.Editora = dto.Editora;
		livro.Edicao = dto.Edicao;
		livro.AnoPublicacao = dto.AnoPublicacao;

		_context.Livros.Update(livro);
		await _context.SaveChangesAsync();

		// Re-link Assuntos and Autores
		await UpdateLivroAssuntosAsync(livro, dto.AssuntoCods);
		await UpdateLivroAutoresAsync(livro, dto.AutorCods);

		return new LivroDto
		{
			Cod = livro.Cod,
			Titulo = livro.Titulo,
			Editora = livro.Editora,
			Edicao = livro.Edicao,
			AnoPublicacao = livro.AnoPublicacao,
			AssuntoCods = dto.AssuntoCods,
			AutorCods = dto.AutorCods
		};
	}

	public async Task<LivroDto> GetByCodAsync(int cod)
	{
		var livro = await _context.Livros
			 .Include(l => l.LivroAssuntos)
			 .FirstOrDefaultAsync(l => l.Cod == cod);
		if (livro == null) return null!;
		return new LivroDto
		{
			Cod = livro.Cod,
			Titulo = livro.Titulo,
			Editora = livro.Editora,
			Edicao = livro.Edicao,
			AnoPublicacao = livro.AnoPublicacao,
			AssuntoCods = livro.LivroAssuntos?.Select(a => a.AssuntoCod).ToList() ?? new List<int>()
		};
	}

	public async Task<List<LivroDto>> GetAllAsync()
	{
		var livros = await _context.Livros
			.Include(l => l.LivroAssuntos)
			.Include(l => l.LivroAutores)
			.ToListAsync();

		return livros.Select(l => new LivroDto
		{
			Cod = l.Cod,
			Titulo = l.Titulo,
			Editora = l.Editora,
			Edicao = l.Edicao,
			AnoPublicacao = l.AnoPublicacao,
			AssuntoCods = l.LivroAssuntos?.Select(a => a.AssuntoCod).ToList() ?? new List<int>(),
			AutorCods = l.LivroAutores?.Select(a => a.AutorCod).ToList() ?? new List<int>()
		}).ToList();
	}
	private async Task UpdateLivroAssuntosAsync(Livro livro, List<int> assuntoCods)
	{
		var existingLinks = _context.LivroAssuntos.Where(la => la.LivroCod == livro.Cod);
		_context.LivroAssuntos.RemoveRange(existingLinks);

		foreach (var assuntoCod in assuntoCods)
		{
			_context.LivroAssuntos.Add(new LivroAssunto
			{
				LivroCod = livro.Cod,
				AssuntoCod = assuntoCod
			});
		}
		await _context.SaveChangesAsync();
	}
	private async Task UpdateLivroAutoresAsync(Livro livro, List<int> autorCods)
	{
		var existing = _context.LivroAutores.Where(la => la.LivroCod == livro.Cod);
		_context.LivroAutores.RemoveRange(existing);

		foreach (var autorCod in autorCods)
		{
			_context.LivroAutores.Add(new LivroAutor
			{
				LivroCod = livro.Cod,
				AutorCod = autorCod
			});
		}
		await _context.SaveChangesAsync();
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