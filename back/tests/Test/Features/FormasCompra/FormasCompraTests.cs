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

        FormaCompra[]? formasCompra = await response.Content.ReadFromJsonAsync<FormaCompra[]>();
        formasCompra.Should().NotBeNull();
    }
}
