namespace JDev.Tuteee.Protos;

using Microsoft.Extensions.DependencyInjection;

public static class Extensions
{
    public static IServiceCollection AddGrpcApiClient(this IServiceCollection services, string uri)
    {
        services.AddGrpcClient<InvoiceCreator.InvoiceCreatorClient>(opts =>
        {
            opts.Address = new Uri(uri);
        });

        return services;
    }
}