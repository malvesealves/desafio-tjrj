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

        Assunto? assunto = await response.Content.ReadFromJsonAsync<Assunto>();
        assunto.Should().NotBeNull();
        assunto!.Descricao.Should().Be("Assunto Teste");
    }

    [Fact]
    public async Task DeveObterAssuntosComSucesso()
    {
        HttpResponseMessage response = await _client.GetAsync("/assuntos");

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        Assunto[]? assuntos = await response.Content.ReadFromJsonAsync<Assunto[]>();
        assuntos.Should().NotBeNull();
    }

    [Fact]
    public async Task DeveDeletarAssuntoComSucesso()
    {
        HttpResponseMessage responseGet = await _client.GetAsync("/assuntos");
        Assunto[]? assuntos = await responseGet.Content.ReadFromJsonAsync<Assunto[]>();

        if (assuntos is not null)
        {
            Assunto assuntoToDelete = assuntos.First();
            HttpResponseMessage responseDelete = await _client.DeleteAsync($"/assuntos/{assuntoToDelete.CodAs}");

            responseDelete.StatusCode.Should().Be(HttpStatusCode.NoContent);

            HttpResponseMessage responseAfterDelete = await _client.GetAsync("/assuntos");
            Assunto[]? assuntosAfterDelete = await responseGet.Content.ReadFromJsonAsync<Assunto[]>();

            Assunto? assuntoDeleted = assuntosAfterDelete?.FirstOrDefault(a => a.CodAs == assuntoToDelete.CodAs);

            assuntoDeleted.Should().BeNull();
        }
    }

    [Fact]
    public async Task DeveAtualizarAssuntoComSucesso()
    {
        HttpResponseMessage responseGet = await _client.GetAsync("/assuntos");
        Assunto[]? assuntos = await responseGet.Content.ReadFromJsonAsync<Assunto[]>();

        if (assuntos is not null)
        {
            Assunto assuntoToUpdate = assuntos.First();

            assuntoToUpdate.Descricao = "Nova Descricao";

            HttpResponseMessage responseUpdate = await _client.PutAsJsonAsync($"/assuntos/{assuntoToUpdate.CodAs}", assuntoToUpdate);

            responseUpdate.StatusCode.Should().Be(HttpStatusCode.OK);

            HttpResponseMessage response = await _client.GetAsync("/assuntos");

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            Assunto[]? assuntosAfterUpdate = await response.Content.ReadFromJsonAsync<Assunto[]>();
            assuntosAfterUpdate.Should().NotBeNull();

            if (assuntosAfterUpdate is not null)
            {
                Assunto? updatedAssunto = assuntosAfterUpdate?.FirstOrDefault(a => a.CodAs == assuntoToUpdate.CodAs);

                updatedAssunto.Should().NotBeNull();
                updatedAssunto!.Descricao.Should().Be("Nova Descricao");
            }            
        }
    }
}
