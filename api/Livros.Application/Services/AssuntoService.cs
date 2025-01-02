using Livros.Data;
using Livros.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

public class AssuntoService
{
	private readonly LivrosContext _context;

	public AssuntoService(LivrosContext context)
	{
		_context = context;
	}

	// CREATE
	public async Task<Assunto> CreateAsync(Assunto assunto)
	{
		_context.Assuntos.Add(assunto);
		await _context.SaveChangesAsync();
		return assunto;
	}

	public async Task<Assunto> GetByCodAsync(int cod)
	{
		return await _context.Assuntos
			 .Include(a => a.LivroAssuntos).ThenInclude(las => las.Livro)
			 .FirstOrDefaultAsync(a => a.Cod == cod);
	}

	// READ (all)
	public async Task<List<Assunto>> GetAllAsync()
	{
		return await _context.Assuntos
			 .Include(a => a.LivroAssuntos).ThenInclude(las => las.Livro)
			 .ToListAsync();
	}

	// UPDATE
	public async Task<Assunto> UpdateAsync(Assunto assunto)
	{
		_context.Assuntos.Update(assunto);
		await _context.SaveChangesAsync();
		return assunto;
	}

	// DELETE
	public async Task<bool> DeleteAsync(int cod)
	{
		var assunto = await _context.Assuntos.FindAsync(cod);
		if (assunto == null) return false;

		_context.Assuntos.Remove(assunto);
		await _context.SaveChangesAsync();
		return true;
	}
}
