using Microsoft.AspNetCore.Mvc;
using Livros.Application.Services;

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

    [HttpGet]
    public IActionResult Get()
    {
        var items = _service.GetRandomLivros();
        return Ok(items);
    }
}
