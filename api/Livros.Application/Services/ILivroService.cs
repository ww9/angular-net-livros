using Livros.Data.Entities;

namespace Livros.Application.Services;

public interface ILivroService
{
	IEnumerable<Livro> GetLivro();
}