using Microsoft.AspNetCore.Mvc.Testing;

namespace WebApp.Integration.Tests;

public sealed class WebAppFixture : IAsyncLifetime
{
    private HttpClient? _httpClient;
    private WebApplicationFactory<Program>? _factory;

    public Task InitializeAsync()
    {
        _factory = new WebApplicationFactory<Program>();
        _httpClient = _factory.CreateClient();
        return Task.CompletedTask;
    }
    
    public HttpClient HttpClient => _httpClient ?? throw new InvalidOperationException("The fixture has not been initialized.");

    public async Task DisposeAsync()
    {
        _httpClient?.Dispose();
        if (_factory is not null)
        {
            await _factory.DisposeAsync();
        }
    }
}