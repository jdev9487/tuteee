namespace JDev.Tuteee.Protos;

using Grpc.Net.ClientFactory;
using Microsoft.Extensions.DependencyInjection;

public static class Extensions
{
    public static IServiceCollection AddGrpcApiClient(this IServiceCollection services, string uri)
    {
        services.AddGrpcClient<Invoice.InvoiceClient>(ConfigureClient);
        services.AddGrpcClient<Lesson.LessonClient>(ConfigureClient);

        return services;

        void ConfigureClient(GrpcClientFactoryOptions opts)
        {
            opts.Address = new Uri(uri);
        }
    }
}