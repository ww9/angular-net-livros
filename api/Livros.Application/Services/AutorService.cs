using Livros.Application.Errors;
using Livros.Data;
using Livros.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Livros.Application.Services;

public class AutorService : IAutorService
{
	private readonly LivrosContext _context;

	public AutorService(LivrosContext context)
	{
		_context = context;
	}

	// CREATE
	public async Task<Autor> CreateAsync(Autor autor)
	{
		// Validar tamanho da descrição
		if (autor.Nome.Length < 3 || autor.Nome.Length > 40)
		{
			throw new ValidationException("Descrição deve ter entre 3 e 40 caracteres");
		}
		// Validar se existe outra descrição igual que não seja a mesma
		var autorExistente = await _context.Autores.FirstOrDefaultAsync(a => a.Nome == autor.Nome);
		if (autorExistente != null)
		{
			throw new ValidationException("Já existe um outro autor com essa descrição");
		}
		_context.Autores.Add(autor);
		await _context.SaveChangesAsync();
		return autor;
	}

	// UPDATE
	public async Task<Autor> UpdateAsync(Autor autor)
	{
		// Validar tamanho da descrição
		if (autor.Nome.Length < 3 || autor.Nome.Length > 40)
		{
			throw new ValidationException("Descrição deve ter entre 3 e 40 caracteres");
		}
		// Validar se existe outra descrição igual que não seja a mesma
		var autorExistente = await _context.Autores.FirstOrDefaultAsync(a => a.Nome == autor.Nome && a.Cod != autor.Cod);
		if (autorExistente != null)
		{
			throw new ValidationException("Já existe um outro autor com essa descrição");
		}
		_context.Autores.Update(autor);
		await _context.SaveChangesAsync();
		return autor;
	}

	public async Task<Autor> GetByCodAsync(int cod)
	{
		return await _context.Autores
			 .Include(a => a.LivroAutores!).ThenInclude(las => las.Livro)
			 .FirstOrDefaultAsync(a => a.Cod == cod) ?? throw new ValidationException("Autor não encontrado.");
	}

	// READ (all)
	public async Task<List<Autor>> GetAllAsync()
	{
		return await _context.Autores
			 .Include(a => a.LivroAutores!).ThenInclude(las => las.Livro)
			 .ToListAsync();
	}

	// DELETE
	public async Task<bool> DeleteAsync(int cod)
	{
		// Se autor não existir com este código, retorna erro.
		var autor = await _context.Autores.FindAsync(cod);
		if (autor == null)
		{
			throw new ValidationException("Autor não encontrado.");
		}

		// Ao tentar excluir um Autor que está associado a um Livro, deve retornar erro.
		var autorComLivros = await _context.Autores
			.Include(a => a.LivroAutores)
			.FirstOrDefaultAsync(a => a.Cod == cod);
		if (autorComLivros?.LivroAutores?.Count > 0)
		{
			throw new ValidationException("Não é possível excluir um autor que está associado a um livro.");
		}

		_context.Autores.Remove(autor);
		await _context.SaveChangesAsync();
		return true;
	}
}
