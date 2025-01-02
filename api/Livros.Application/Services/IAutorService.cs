using Livros.Data;
using Livros.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Livros.Application.Services;
public interface IAutorService
{
	Task<Autor> CreateAsync(Autor autor);
	Task<Autor> GetByCodAsync(int cod);
	Task<List<Autor>> GetAllAsync();
	Task<Autor> UpdateAsync(Autor autor);
	Task<bool> DeleteAsync(int cod);
}