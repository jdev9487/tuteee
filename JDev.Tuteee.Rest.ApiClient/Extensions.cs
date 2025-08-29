namespace JDev.Tuteee.Rest.ApiClient;

using Microsoft.Extensions.DependencyInjection;

public static class Extensions
{
    public static IServiceCollection AddApiClient(this IServiceCollection services, string uri)
    {
        services.AddHttpClient<IRestApiClient, RestApiClient>(client =>
        {
            client.BaseAddress = new Uri(uri);
        });
        return services;
    }
}