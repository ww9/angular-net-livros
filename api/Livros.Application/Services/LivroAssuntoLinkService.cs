using Livros.Data;
using Livros.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class LivroAssuntoLinkService
{
	private readonly LivrosContext _context;

	public LivroAssuntoLinkService(LivrosContext context)
	{
		_context = context;
	}

	public async Task LinkAssuntoToLivro(int livroCod, int assuntoCod)
	{
		bool alreadyLinked = await _context.LivroAssuntos
			 .AnyAsync(la => la.LivroCod == livroCod && la.AssuntoCod == assuntoCod);

		if (!alreadyLinked)
		{
			var link = new LivroAssunto
			{
				LivroCod = livroCod,
				AssuntoCod = assuntoCod
			};
			_context.LivroAssuntos.Add(link);
			await _context.SaveChangesAsync();
		}
	}

	/// <summary>
	/// Removes the link between a Livro and an Assunto, if it exists.
	/// </summary>
	public async Task UnlinkAssuntoFromLivro(int livroCod, int assuntoCod)
	{
		var link = await _context.LivroAssuntos
			 .FirstOrDefaultAsync(la => la.LivroCod == livroCod && la.AssuntoCod == assuntoCod);

		if (link != null)
		{
			_context.LivroAssuntos.Remove(link);
			await _context.SaveChangesAsync();
		}
	}

	/// <summary>
	/// Gets all Assuntos linked to a given Livro.
	/// </summary>
	public async Task<List<Assunto>> ListAssuntosDeLivro(int livroCod)
	{
		return await _context.LivroAssuntos
			 .Where(la => la.LivroCod == livroCod)
			 .Include(la => la.Assunto)
			 .Select(la => la.Assunto)
			 .ToListAsync();
	}

	/// <summary>
	/// Gets all Livros linked to a given Assunto.
	/// </summary>
	public async Task<List<Livro>> ListLivrosDeAssunto(int assuntoCod)
	{
		return await _context.LivroAssuntos
			 .Where(la => la.AssuntoCod == assuntoCod)
			 .Include(la => la.Livro)
			 .Select(la => la.Livro)
			 .ToListAsync();
	}
}
