namespace JDev.Tuteee.ApiClient;

using Microsoft.Extensions.DependencyInjection;

public static class Extensions
{
    public static IServiceCollection AddApiClient(this IServiceCollection services, string uri)
    {
        services.AddHttpClient<IApiClient, ApiClient>(client =>
        {
            client.BaseAddress = new Uri(uri);
        });
        return services;
    }
}