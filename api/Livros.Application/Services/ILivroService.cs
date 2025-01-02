using Livros.Data.Entities;

namespace Livros.Application.Services;

public interface ILivroService
{
	Task<Livro> CreateAsync(Livro livro);
	Task<Livro> GetByCodAsync(int cod);
	Task<List<Livro>> GetAllAsync();
	Task<Livro> UpdateAsync(Livro livro);
	Task<bool> DeleteAsync(int cod);
	IEnumerable<Livro> GetRandomLivros();
}