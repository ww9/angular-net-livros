using Microsoft.AspNetCore.Mvc;
using Livros.Application.Services;
using Livros.Data.Entities;
using Livros.API.Helpers;
using Livros.Application.Errors;

namespace Autors.API.Controllers;

[ApiController]
[Route("[controller]")]
public class AutorController : ControllerBase
{
    private readonly IAutorService _service;

    public AutorController(IAutorService autorService)
    {
        _service = autorService;
    }

    // CREATE
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Autor autor)
    {
        if (autor == null)
        {
            return BadRequest();
        }

        var createdAutor = await _service.CreateAsync(autor);
        return Ok(new { cod = createdAutor.Cod });
    }

    // READ (by cod)
    [HttpGet("{cod}")]
    public async Task<IActionResult> GetByCodAsync(int cod)
    {
        var autor = await _service.GetByCodAsync(cod);
        if (autor == null)
        {
            return NotFound();
        }

        return Ok(autor);
    }

    // READ (all)
    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var autors = await _service.GetAllAsync();
        return Ok(autors);
    }

    // UPDATE
    [HttpPut("{cod}")]
    public async Task<IActionResult> UpdateAsync(int cod, [FromBody] Autor autor)
    {
        if (autor == null || autor.Cod != cod)
        {
            return BadRequest();
        }
        var updatedAutor = await _service.UpdateAsync(autor);
        return Ok(updatedAutor);
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
