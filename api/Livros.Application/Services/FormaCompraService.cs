using Livros.Application.Errors;
using Livros.Data;
using Livros.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Livros.Application.Services;

public class FormaCompraService : IFormaCompraService
{
	private readonly LivrosContext _context;

	public FormaCompraService(LivrosContext context)
	{
		_context = context;
	}

	// CREATE
	public async Task<FormaCompra> CreateAsync(FormaCompra formacompra)
	{
		// Validar tamanho da descrição
		if (formacompra.Descricao.Length < 3 || formacompra.Descricao.Length > 40)
		{
			throw new ValidationException("Descrição deve ter entre 3 e 40 caracteres");
		}
		// Validar se existe outra descrição igual que não seja a mesma
		var formacompraExistente = await _context.FormaCompras.FirstOrDefaultAsync(a => a.Descricao == formacompra.Descricao);
		if (formacompraExistente != null)
		{
			throw new ValidationException("Já existe uma outra forma de compra com esta descrição");
		}
		_context.FormaCompras.Add(formacompra);
		await _context.SaveChangesAsync();
		return formacompra;
	}

	// UPDATE
	public async Task<FormaCompra> UpdateAsync(FormaCompra formacompra)
	{
		// Validar tamanho da descrição
		if (formacompra.Descricao.Length < 3 || formacompra.Descricao.Length > 40)
		{
			throw new ValidationException("Descrição deve ter entre 3 e 40 caracteres");
		}
		// Validar se existe outra descrição igual que não seja a mesma
		var formacompraExistente = await _context.FormaCompras.FirstOrDefaultAsync(a => a.Descricao == formacompra.Descricao && a.Cod != formacompra.Cod);
		if (formacompraExistente != null)
		{
			throw new ValidationException("Já existe uma outra forma de compra com esta descrição");
		}
		_context.FormaCompras.Update(formacompra);
		await _context.SaveChangesAsync();
		return formacompra;
	}

	public async Task<FormaCompra> GetByCodAsync(int cod)
	{
		return await _context.FormaCompras
			 .Include(a => a.LivroFormaCompras).ThenInclude(las => las.Livro)
			 .FirstOrDefaultAsync(a => a.Cod == cod);
	}

	// READ (all)
	public async Task<List<FormaCompra>> GetAllAsync()
	{
		return await _context.FormaCompras
			 .Include(a => a.LivroFormaCompras).ThenInclude(las => las.Livro)
			 .ToListAsync();
	}

	// DELETE
	public async Task<bool> DeleteAsync(int cod)
	{
		// Se formacompra não existir com este código, retorna erro.
		var formacompra = await _context.FormaCompras.FindAsync(cod);
		if (formacompra == null)
		{
			throw new ValidationException("Forma de compra não encontrada.");
		}

		// Ao tentar excluir uma Forma de Compra que está associado a um Livro, deve retornar erro.
		var formacompraComLivros = await _context.FormaCompras
			.Include(a => a.LivroFormaCompras)
			.FirstOrDefaultAsync(a => a.Cod == cod);
		if (formacompraComLivros?.LivroFormaCompras?.Count > 0)
		{
			throw new ValidationException("Não é possível excluir uma forma de compra que está associada a um livro.");
		}

		_context.FormaCompras.Remove(formacompra);
		await _context.SaveChangesAsync();
		return true;
	}
}
