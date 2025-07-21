namespace JDev.Tuteee.ApiClient;

using Microsoft.Extensions.DependencyInjection;

public static class Extensions
{
    public static IServiceCollection AddApiClient(this IServiceCollection services)
    {
        services.AddHttpClient<IApiClient, ApiClient>(client =>
        {
            client.BaseAddress = new Uri("http://localhost:5078/");
        });
        return services;
    }
}