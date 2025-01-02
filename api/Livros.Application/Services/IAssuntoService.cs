using Livros.Data;
using Livros.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Livros.Application.Services;
public interface IAssuntoService
{
	Task<Assunto> CreateAsync(Assunto assunto);
	Task<Assunto> GetByCodAsync(int cod);
	Task<List<Assunto>> GetAllAsync();
	Task<Assunto> UpdateAsync(Assunto assunto);
	Task<bool> DeleteAsync(int cod);
}