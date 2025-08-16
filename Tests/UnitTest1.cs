namespace Tests;

using JDev.Tuteee.ApiClient;
using Microsoft.Extensions.DependencyInjection;

public class Tests
{
    [Test]
    public async Task Test1()
    {
        var services = new ServiceCollection();
        services.AddApiClient("http://localhost:5078");
        var provider = services.BuildServiceProvider();
        var api = provider.GetRequiredService<IApiClient>();
        var clients = await api.GetClientsAsync();
    }
}