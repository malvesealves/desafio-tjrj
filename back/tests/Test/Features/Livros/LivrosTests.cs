using API.Models;
using FluentAssertions;
using System.Net;
using System.Net.Http.Json;
using Test.Factory;

namespace Test.Features.Livros;

public class LivrosTests(CustomWebAppFactory factory) : IClassFixture<CustomWebAppFactory>
{
    private readonly HttpClient _client = factory.CreateClient();

    [Fact(DisplayName = "Cria novo registro de Livro com sucesso")]
    public async Task DeveCriarLivroComSucesso()
    {
        Livro novoLivro = new()
        { 
            Titulo = "Livro Teste", 
            AnoPublicacao = "2025", 
            Assunto = new Assunto() ,
            FormaCompra = new FormaCompra()
        };

        HttpResponseMessage response = await _client.PostAsJsonAsync("/livros", novoLivro);

        response.StatusCode.Should().Be(HttpStatusCode.Created);

        Livro? livro = await response.Content.ReadFromJsonAsync<Livro>();
        livro.Should().NotBeNull();
        livro!.Titulo.Should().Be("Livro Teste");
    }

    [Fact(DisplayName = "Busca todos os registros de Livro cadastrados")]
    public async Task DeveObterLivrosComSucesso()
    {
        HttpResponseMessage response = await _client.GetAsync("/livros");

        response.StatusCode.Should().Be(HttpStatusCode.Created);

        Livro[]? livros = await response.Content.ReadFromJsonAsync<Livro[]>();
        livros.Should().NotBeNull();
    }

    [Fact(DisplayName = "Deleta registro de Livro com sucesso e valida não existência")]
    public async Task DeveDeletarLivroComSucesso()
    {
        HttpResponseMessage responseGet = await _client.GetAsync("/livros");
        Livro[]? livros = await responseGet.Content.ReadFromJsonAsync<Livro[]>();

        if (livros is not null)
        {
            Livro livroToDelete = livros.First();
            HttpResponseMessage responseDelete = await _client.DeleteAsync($"/livros/{livroToDelete.CodL}");

            responseDelete.StatusCode.Should().Be(HttpStatusCode.NoContent);

            HttpResponseMessage responseAfterDelete = await _client.GetAsync("/livros");
            Livro[]? livrosAfterDelete = await responseGet.Content.ReadFromJsonAsync<Livro[]>();

            Livro? livroDeleted = livrosAfterDelete?.FirstOrDefault(a => a.CodL == livroToDelete.CodL);

            livroDeleted.Should().BeNull();
        }
    }
}