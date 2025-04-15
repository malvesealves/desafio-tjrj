using API.Models;
using FluentAssertions;
using System.Net;
using System.Net.Http.Json;
using Test.Factory;

namespace Test.Features.Assuntos;

public class AssuntosTests(CustomWebAppFactory factory) : IClassFixture<CustomWebAppFactory>
{
    private readonly HttpClient _client = factory.CreateClient();

    [Fact]
    public async Task DeveCriarAssuntoComSucesso()
    {
        Assunto novoAssunto = new()
        {
            Descricao = "Assunto Teste",
        };

        HttpResponseMessage response = await _client.PostAsJsonAsync("/assuntos", novoAssunto);

        response.StatusCode.Should().Be(HttpStatusCode.Created);

        Livro? livro = await response.Content.ReadFromJsonAsync<Livro>();
        livro.Should().NotBeNull();
        livro!.Titulo.Should().Be("Assunto Teste");
    }

    [Fact]
    public async Task DeveDeletarAssuntoComSucesso()
    {
        

        HttpResponseMessage response = await _client.PostAsJsonAsync("/assuntos", novoAssunto);

        response.StatusCode.Should().Be(HttpStatusCode.Created);

        Livro? livro = await response.Content.ReadFromJsonAsync<Livro>();
        livro.Should().NotBeNull();
        livro!.Titulo.Should().Be("Assunto Teste");
    }

    [Fact]
    public async Task DeveObterAssuntosComSucesso()
    {
        HttpResponseMessage response = await _client.GetAsync("/assuntos");

        response.StatusCode.Should().Be(HttpStatusCode.Created);

        Assunto[]? livros = await response.Content.ReadFromJsonAsync<Assunto[]>();
        livros.Should().NotBeNull();
    }
}
