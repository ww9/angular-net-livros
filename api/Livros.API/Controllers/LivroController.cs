using Microsoft.AspNetCore.Mvc;
using Livros.Application.Services;

namespace Livros.API.Controllers;

[ApiController]
[Route("[controller]")]
public class LivroController : ControllerBase
{
    private readonly ILivroService _livroService;

    public LivroController(ILivroService livroService)
    {
        _livroService = livroService;
    }

    [HttpGet]
    public IActionResult Get()
    {
        var forecasts = _livroService.GetLivro();
        return Ok(forecasts);
    }
}
