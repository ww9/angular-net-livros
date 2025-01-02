using Microsoft.AspNetCore.Mvc;
using Livros.Application.Services;
using Livros.Data.Entities;
using Livros.API.Helpers;
using Livros.Application.Errors;

namespace Livros.API.Controllers;

[ApiController]
[Route("[controller]")]
public class LivroController : ControllerBase
{
    private readonly ILivroService _service;

    public LivroController(ILivroService livroService)
    {
        _service = livroService;
    }

    // CREATE
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Livro livro)
    {
        if (livro == null)
        {
            return BadRequest();
        }

        var createdLivro = await _service.CreateAsync(livro);
        return Ok(new { cod = createdLivro.Cod });
    }

    // READ (by cod)
    [HttpGet("{cod}")]
    public async Task<IActionResult> GetByCodAsync(int cod)
    {
        var livro = await _service.GetByCodAsync(cod);
        if (livro == null)
        {
            return NotFound();
        }

        return Ok(livro);
    }

    // READ (all)
    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var livros = await _service.GetAllAsync();
        return Ok(livros);
    }

    // UPDATE
    [HttpPut("{cod}")]
    public async Task<IActionResult> UpdateAsync(int cod, [FromBody] Livro livro)
    {
        if (livro == null || livro.Cod != cod)
        {
            return BadRequest();
        }
        var updatedLivro = await _service.UpdateAsync(livro);
        return Ok(updatedLivro);
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

    [HttpGet("random")]
    public IActionResult Get()
    {
        var items = _service.GetRandomLivros();
        return Ok(items);
    }
}
