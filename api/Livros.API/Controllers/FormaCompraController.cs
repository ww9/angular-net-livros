using Microsoft.AspNetCore.Mvc;
using Livros.Application.Services;
using Livros.Data.Entities;
using Livros.API.Helpers;
using Livros.Application.Errors;

namespace FormaCompras.API.Controllers;

[ApiController]
[Route("[controller]")]
public class FormaCompraController : ControllerBase
{
    private readonly IFormaCompraService _service;

    public FormaCompraController(IFormaCompraService formacompraService)
    {
        _service = formacompraService;
    }

    // CREATE
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] FormaCompra formacompra)
    {
        if (formacompra == null)
        {
            return BadRequest();
        }

        var createdFormaCompra = await _service.CreateAsync(formacompra);
        return Ok(new { cod = createdFormaCompra.Cod });
    }

    // READ (by cod)
    [HttpGet("{cod}")]
    public async Task<IActionResult> GetByCodAsync(int cod)
    {
        var formacompra = await _service.GetByCodAsync(cod);
        if (formacompra == null)
        {
            return NotFound();
        }

        return Ok(formacompra);
    }

    // READ (all)
    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var formacompras = await _service.GetAllAsync();
        return Ok(formacompras);
    }

    // UPDATE
    [HttpPut("{cod}")]
    public async Task<IActionResult> UpdateAsync(int cod, [FromBody] FormaCompra formacompra)
    {
        if (formacompra == null || formacompra.Cod != cod)
        {
            return BadRequest();
        }
        var updatedFormaCompra = await _service.UpdateAsync(formacompra);
        return Ok(updatedFormaCompra);
    }

    // DELETE
    [HttpDelete("{cod}")]
    public async Task<IActionResult> DeleteAsync(int cod)
    {
        var result = await _service.DeleteAsync(cod);
        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
