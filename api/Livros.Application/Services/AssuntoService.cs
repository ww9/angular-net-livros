using Livros.Application.Errors;
using Livros.Data;
using Livros.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Livros.Application.Services;

public class AssuntoService : IAssuntoService
{
	private readonly LivrosContext _context;

	public AssuntoService(LivrosContext context)
	{
		_context = context;
	}

	// CREATE
	public async Task<Assunto> CreateAsync(Assunto assunto)
	{
		// Validar tamanho da descrição
		if (assunto.Descricao.Length < 3 || assunto.Descricao.Length > 40)
		{
			throw new ValidationException("Descrição deve ter entre 3 e 40 caracteres");
		}
		// Validar se existe outra descrição igual que não seja a mesma
		var assuntoExistente = await _context.Assuntos.FirstOrDefaultAsync(a => a.Descricao == assunto.Descricao);
		if (assuntoExistente != null)
		{
			throw new ValidationException("Já existe um outro assunto com essa descrição");
		}
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
		// Validar tamanho da descrição
		if (assunto.Descricao.Length < 3 || assunto.Descricao.Length > 40)
		{
			throw new ValidationException("Descrição deve ter entre 3 e 40 caracteres");
		}
		// Validar se existe outra descrição igual que não seja a mesma
		var assuntoExistente = await _context.Assuntos.FirstOrDefaultAsync(a => a.Descricao == assunto.Descricao && a.Cod != assunto.Cod);
		if (assuntoExistente != null)
		{
			throw new ValidationException("Já existe um outro assunto com essa descrição");
		}
		_context.Assuntos.Update(assunto);
		await _context.SaveChangesAsync();
		return assunto;
	}

	// DELETE
	public async Task<bool> DeleteAsync(int cod)
	{
		// Se assunto não existir com este código, retorna erro.
		var assunto = await _context.Assuntos.FindAsync(cod);
		if (assunto == null)
		{
			throw new ValidationException("Assunto não encontrado.");
		}

		// Ao tentar excluir um Assunto que está associado a um Livro, deve retornar erro.
		var assuntoComLivros = await _context.Assuntos
			.Include(a => a.LivroAssuntos)
			.FirstOrDefaultAsync(a => a.Cod == cod);
		if (assuntoComLivros?.LivroAssuntos?.Count > 0)
		{
			throw new ValidationException("Não é possível excluir um assunto que está associado a um livro.");
		}

		_context.Assuntos.Remove(assunto);
		await _context.SaveChangesAsync();
		return true;
	}
}
