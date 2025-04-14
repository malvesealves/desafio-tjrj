using API.Models;
using FluentAssertions;
using System.Net;
using System.Net.Http.Json;
using Test.Factory;

namespace Test.Features.Autores;

public class AutoresTests(CustomWebAppFactory factory) : IClassFixture<CustomWebAppFactory>
{
    private readonly HttpClient _client = factory.CreateClient();

    [Fact]
    public async Task DeveCriarAssuntoComSucesso()
    {
        Autor novoAutor = new()
        {
            Nome = "Nome Teste",
        };

        HttpResponseMessage response = await _client.PostAsJsonAsync("/autores", novoAutor);

        response.StatusCode.Should().Be(HttpStatusCode.Created);

        Livro? livro = await response.Content.ReadFromJsonAsync<Livro>();
        livro.Should().NotBeNull();
        livro!.Titulo.Should().Be("Nome Teste");
    }

    [Fact]
    public async Task DeveObterAssuntosComSucesso()
    {
        HttpResponseMessage response = await _client.GetAsync("/autores");

        response.StatusCode.Should().Be(HttpStatusCode.Created);

        Assunto[]? livros = await response.Content.ReadFromJsonAsync<Assunto[]>();
        livros.Should().NotBeNull();
    }
}
