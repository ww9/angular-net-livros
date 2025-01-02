using Microsoft.AspNetCore.Mvc;
using Livros.Application.Services;
using Livros.Data.Entities;
using Livros.API.Helpers;
using Livros.Application.Errors;
using Livros.Application.Dtos;

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

    // CREATE
    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] LivroDto livroDto)
    {
        if (livroDto == null)
        {
            return BadRequest();
        }
        var created = await _service.CreateAsync(livroDto);
        return Ok(created);
    }

    // UPDATE
    [HttpPut("{cod}")]
    public async Task<IActionResult> UpdateAsync(int cod, [FromBody] LivroDto livroDto)
    {
        if (livroDto == null || livroDto.Cod != cod)
        {
            return BadRequest();
        }
        var updatedLivro = await _service.UpdateAsync(livroDto);
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
