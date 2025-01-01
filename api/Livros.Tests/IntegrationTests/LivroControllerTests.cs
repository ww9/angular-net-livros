using System.Net.Http.Json;
using Livros.Data.Entities;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Livros.Tests.IntegrationTests;

public class LivroControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly HttpClient _client;

    public LivroControllerTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Get_ReturnsLivro()
    {
        var response = await _client.GetAsync("/livro");
        response.EnsureSuccessStatusCode();
        var forecasts = await response.Content.ReadFromJsonAsync<IEnumerable<Livro>>();
        Assert.NotNull(forecasts);
        Assert.NotEmpty(forecasts);
    }
}
