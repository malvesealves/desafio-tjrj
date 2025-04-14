using API.Models;
using FluentAssertions;
using System.Net;
using System.Net.Http.Json;
using Test.Factory;

namespace Test.Features.FormasCompra;

public class FormasCompraTests : IClassFixture<CustomWebAppFactory>
{
    private readonly HttpClient _client;

    public FormasCompraTests(CustomWebAppFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task DeveObterFormasCompraComSucesso()
    {
        HttpResponseMessage response = await _client.GetAsync("/formas-compra");

        response.StatusCode.Should().Be(HttpStatusCode.Created);

        Assunto[]? livros = await response.Content.ReadFromJsonAsync<Assunto[]>();
        livros.Should().NotBeNull();
    }
}
