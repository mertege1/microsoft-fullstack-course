using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace SafeVault.Tests;

public class SecurityTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public SecurityTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task PostVault_WithoutToken_ReturnsUnauthorized()
    {
        var response = await _client.PostAsJsonAsync("/api/vault", new { Title = "Gizli", Content = "Veri" });
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task GetSecrets_WithStandardUser_ReturnsForbidden()
    {
        var loginResponse = await _client.PostAsJsonAsync("/api/login", new { Username = "user", Password = "user123" });
        var loginData = await loginResponse.Content.ReadFromJsonAsync<JsonElement>();
        
        // BÜYÜK "T" YERİNE KÜÇÜK "t" YAPTIK
        var token = loginData.GetProperty("token").GetString();

        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token!);

        var response = await _client.GetAsync("/api/vault/secrets");
        Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
    }

    [Fact]
    public async Task PostVault_WithInvalidCharacters_ReturnsBadRequest()
    {
        var loginResponse = await _client.PostAsJsonAsync("/api/login", new { Username = "admin", Password = "admin123" });
        var loginData = await loginResponse.Content.ReadFromJsonAsync<JsonElement>();
        
        // BÜYÜK "T" YERİNE KÜÇÜK "t" YAPTIK
        var token = loginData.GetProperty("token").GetString();

        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token!);

        var badPayload = new { Title = "<script>DROP</script>", Content = "Zararlı" };
        var response = await _client.PostAsJsonAsync("/api/vault", badPayload);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}