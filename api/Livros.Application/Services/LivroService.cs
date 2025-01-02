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
}