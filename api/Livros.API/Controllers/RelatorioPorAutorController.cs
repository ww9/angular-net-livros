// ...existing code...
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Livros.Data;
using Livros.Data.Entities;

namespace Livros.API.Controllers;

[ApiController]
[Route("[controller]")]
public class RelatorioPorAutorController : ControllerBase
{
	private readonly LivrosContext _context;

	public RelatorioPorAutorController(LivrosContext context)
	{
		_context = context;
	}

	[HttpGet]
	public async Task<IActionResult> GetRelatorio()
	{
		// Agrupa por Autor e lista Livros, Assuntos
		var resultado = await _context.LivroAutores
			 .Include(la => la.Livro).ThenInclude(l => l.LivroAssuntos).ThenInclude(la => la.Assunto)
			 .Include(la => la.Autor)
			 .Select(la => new
			 {
				 AutorNome = la.Autor.Nome,
				 LivroCod = la.LivroCod,
				 LivroTitulo = la.Livro.Titulo,
				 LivroEditora = la.Livro.Editora,
				 LivroEdicao = la.Livro.Edicao,
				 LivroAno = la.Livro.AnoPublicacao,
				 Assuntos = la.Livro.LivroAssuntos.Select(a => a.Assunto.Descricao).ToList()
			 })
			 .OrderBy(x => x.AutorNome)
			 .ThenBy(x => x.LivroCod)
			 .ToListAsync();

		return Ok(resultado);
	}
}