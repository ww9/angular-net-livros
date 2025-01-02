using Livros.Data;
using Livros.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class LivroAutorLinkService
{
	private readonly LivrosContext _context;

	public LivroAutorLinkService(LivrosContext context)
	{
		_context = context;
	}

	public async Task LinkAutorToLivro(int livroCod, int autorCod)
	{
		bool alreadyLinked = await _context.LivroAutores
			 .AnyAsync(la => la.LivroCod == livroCod && la.AutorCod == autorCod);

		if (!alreadyLinked)
		{
			var link = new LivroAutor
			{
				LivroCod = livroCod,
				AutorCod = autorCod
			};
			_context.LivroAutores.Add(link);
			await _context.SaveChangesAsync();
		}
	}

	public async Task UnlinkAutorFromLivro(int livroCod, int autorCod)
	{
		var link = await _context.LivroAutores
			 .FirstOrDefaultAsync(la => la.LivroCod == livroCod && la.AutorCod == autorCod);

		if (link != null)
		{
			_context.LivroAutores.Remove(link);
			await _context.SaveChangesAsync();
		}
	}

	public async Task<List<Autor>> ListAutoresDeLivro(int livroCod)
	{
		return await _context.LivroAutores
			 .Where(la => la.LivroCod == livroCod)
			 .Include(la => la.Autor)
			 .Select(la => la.Autor)
			 .ToListAsync();
	}

	public async Task<List<Livro>> ListLivrosDeAutor(int autorCod)
	{
		return await _context.LivroAutores
			 .Where(la => la.AutorCod == autorCod)
			 .Include(la => la.Livro)
			 .Select(la => la.Livro)
			 .ToListAsync();
	}
}
