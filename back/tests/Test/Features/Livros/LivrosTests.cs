using API.Models;
using FluentAssertions;
using System.Net;
using System.Net.Http.Json;
using Test.Factory;

namespace Test.Features.Livros;

public class LivrosTests(CustomWebAppFactory factory) : IClassFixture<CustomWebAppFactory>
{
    private readonly HttpClient _client = factory.CreateClient();

    [Fact]
    public async Task DeveCriarLivroComSucesso()
    {
        Livro novoLivro = new()
        { 
            Titulo = "Livro Teste", 
            AnoPublicacao = "2025", 
            Assunto = new Assunto() 
        };

        HttpResponseMessage response = await _client.PostAsJsonAsync("/livros", novoLivro);

        response.StatusCode.Should().Be(HttpStatusCode.Created);

        Livro? livro = await response.Content.ReadFromJsonAsync<Livro>();
        livro.Should().NotBeNull();
        livro!.Titulo.Should().Be("Livro Teste");
    }

    [Fact]
    public async Task DeveObterLivrosComSucesso()
    {
        HttpResponseMessage response = await _client.GetAsync("/livros");

        response.StatusCode.Should().Be(HttpStatusCode.Created);

        Livro[]? livros = await response.Content.ReadFromJsonAsync<Livro[]>();
        livros.Should().NotBeNull();
    }
}