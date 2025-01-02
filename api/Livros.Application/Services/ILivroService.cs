using Livros.Application.Dtos;
using Livros.Data.Entities;

namespace Livros.Application.Services;

public interface ILivroService
{
	Task<LivroDto> CreateAsync(LivroDto livro);
	Task<LivroDto> GetByCodAsync(int cod);
	Task<List<LivroDto>> GetAllAsync();
	Task<LivroDto> UpdateAsync(LivroDto dto);
	Task<bool> DeleteAsync(int cod);
	IEnumerable<LivroDto> GetRandomLivros();
}