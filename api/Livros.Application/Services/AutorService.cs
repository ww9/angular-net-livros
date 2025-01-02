using Livros.Data;
using Livros.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Livros.Application.Services;

public class AutorService
{
	private readonly LivrosContext _context;

	public AutorService(LivrosContext context)
	{
		_context = context;
	}

	public async Task<Autor> CreateAsync(Autor autor)
	{
		_context.Autores.Add(autor);
		await _context.SaveChangesAsync();
		return autor;
	}

	public async Task<Autor> GetByCodAsync(int cod)
	{
		return await _context.Autores
			 .Include(a => a.LivroAutores).ThenInclude(la => la.Livro)
			 .FirstOrDefaultAsync(a => a.Cod == cod);
	}

	// READ (all)
	public async Task<List<Autor>> GetAllAsync()
	{
		return await _context.Autores
			 .Include(a => a.LivroAutores).ThenInclude(la => la.Livro)
			 .ToListAsync();
	}

	// UPDATE
	public async Task<Autor> UpdateAsync(Autor autor)
	{
		_context.Autores.Update(autor);
		await _context.SaveChangesAsync();
		return autor;
	}

	// DELETE
	public async Task<bool> DeleteAsync(int cod)
	{
		var autor = await _context.Autores.FindAsync(cod);
		if (autor == null) return false;

		_context.Autores.Remove(autor);
		await _context.SaveChangesAsync();
		return true;
	}
}
