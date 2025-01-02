using Livros.Data;
using Livros.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Livros.Application.Services;
public interface IFormaCompraService
{
	Task<FormaCompra> CreateAsync(FormaCompra formacompra);
	Task<FormaCompra> GetByCodAsync(int cod);
	Task<List<FormaCompra>> GetAllAsync();
	Task<FormaCompra> UpdateAsync(FormaCompra formacompra);
	Task<bool> DeleteAsync(int cod);
}