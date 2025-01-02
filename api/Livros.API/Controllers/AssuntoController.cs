using Microsoft.AspNetCore.Mvc;
using Livros.Application.Services;
using Livros.Data.Entities;

namespace Assuntos.API.Controllers;

[ApiController]
[Route("[controller]")]
public class AssuntoController : ControllerBase
{
    private readonly IAssuntoService _service;

    public AssuntoController(IAssuntoService assuntoService)
    {
        _service = assuntoService;
    }

    // CREATE
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Assunto assunto)
    {
        if (assunto == null)
        {
            return BadRequest();
        }

        var createdAssunto = await _service.CreateAsync(assunto);
        return Ok(new { cod = createdAssunto.Cod });
    }

    // READ (by cod)
    [HttpGet("{cod}")]
    public async Task<IActionResult> GetByCodAsync(int cod)
    {
        var assunto = await _service.GetByCodAsync(cod);
        if (assunto == null)
        {
            return NotFound();
        }

        return Ok(assunto);
    }

    // READ (all)
    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var assuntos = await _service.GetAllAsync();
        return Ok(assuntos);
    }

    // UPDATE
    [HttpPut("{cod}")]
    public async Task<IActionResult> UpdateAsync(int cod, [FromBody] Assunto assunto)
    {
        if (assunto == null || assunto.Cod != cod)
        {
            return BadRequest();
        }

        var updatedAssunto = await _service.UpdateAsync(assunto);
        return Ok(updatedAssunto);
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
