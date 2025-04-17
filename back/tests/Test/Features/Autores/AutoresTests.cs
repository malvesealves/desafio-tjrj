using API.Models;
using FluentAssertions;
using System.Net;
using System.Net.Http.Json;
using Test.Factory;

namespace Test.Features.Autores;

public class AutoresTests(CustomWebAppFactory factory) : IClassFixture<CustomWebAppFactory>
{
    private readonly HttpClient _client = factory.CreateClient();

    [Fact(DisplayName = "Cria novo registro de Autor com sucesso")]
    public async Task DeveCriarAutorComSucesso()
    {
        Autor novoAutor = new()
        {
            Nome = "Autor Teste",
        };

        HttpResponseMessage response = await _client.PostAsJsonAsync("/autores", novoAutor);

        response.StatusCode.Should().Be(HttpStatusCode.Created);

        Autor? autor = await response.Content.ReadFromJsonAsync<Autor>();
        autor.Should().NotBeNull();
        autor!.Nome.Should().Be("Autor Teste");
    }

    [Fact(DisplayName = "Busca todos os registros de Autor cadastrados")]
    public async Task DeveObterAutoresComSucesso()
    {
        HttpResponseMessage response = await _client.GetAsync("/autores");

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        Autor[]? autores = await response.Content.ReadFromJsonAsync<Autor[]>();
        autores.Should().NotBeNull();
    }

    [Fact(DisplayName = "Deleta registro de Autor com sucesso e valida não existência")]
    public async Task DeveDeletarAutorComSucesso()
    {
        HttpResponseMessage responseGet = await _client.GetAsync("/autores");
        Autor[]? autores = await responseGet.Content.ReadFromJsonAsync<Autor[]>();

        if (autores is not null)
        {
            Autor autorToDelete = autores.First();
            HttpResponseMessage responseDelete = await _client.DeleteAsync($"/autores/{autorToDelete.CodAu}");

            responseDelete.StatusCode.Should().Be(HttpStatusCode.NoContent);

            HttpResponseMessage responseAfterDelete = await _client.GetAsync("/autores");
            Autor[]? autoresAfterDelete = await responseGet.Content.ReadFromJsonAsync<Autor[]>();

            Autor? autorDeleted = autoresAfterDelete?.FirstOrDefault(a => a.CodAu == autorToDelete.CodAu);

            autorDeleted.Should().BeNull();
        }
    }

    [Fact(DisplayName = "Atualiza registro de Autor com sucesso")]
    public async Task DeveAtualizarAutorComSucesso()
    {
        HttpResponseMessage responseGet = await _client.GetAsync("/autores");
        Autor[]? autores = await responseGet.Content.ReadFromJsonAsync<Autor[]>();

        if (autores is not null)
        {
            Autor autorToUpdate = autores.First();

            autorToUpdate.Nome = "Novo Nome";

            HttpResponseMessage responseUpdate = await _client.PutAsJsonAsync($"/autores/{autorToUpdate.CodAu}", autorToUpdate);

            responseUpdate.StatusCode.Should().Be(HttpStatusCode.OK);

            HttpResponseMessage response = await _client.GetAsync("/autores");

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            Autor[]? autoresAfterUpdate = await response.Content.ReadFromJsonAsync<Autor[]>();
            autoresAfterUpdate.Should().NotBeNull();

            if (autoresAfterUpdate is not null)
            {
                Autor? updatedAutor = autoresAfterUpdate?.FirstOrDefault(a => a.CodAu == autorToUpdate.CodAu);

                updatedAutor.Should().NotBeNull();
                updatedAutor!.Nome.Should().Be("Novo Nome");
            }
        }
    }
}
