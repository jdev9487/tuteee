namespace JDev.Tuteee.Api.Extensions;

using Endpoints;
using Microsoft.Extensions.DependencyInjection.Extensions;

public static class Extensions
{
    public static IServiceCollection AddEndpoints(this IServiceCollection services)
    {
        var assembly = typeof(Program).Assembly;
        var serviceDescriptors = assembly
            .DefinedTypes
            .Where(type => type is { IsAbstract: false, IsInterface: false } &&
                           type.IsAssignableTo(typeof(IEndpoints)))
            .Select(type => ServiceDescriptor.Transient(typeof(IEndpoints), type));
        services.TryAddEnumerable(serviceDescriptors);
        return services;
    }
    public static IApplicationBuilder RegisterEndpoints(this WebApplication app)
    {
        var endpoints = app.Services.GetRequiredService<IEnumerable<IEndpoints>>();
        foreach (var endpoint in endpoints)
        {
            endpoint.MapRoutes(app);
        }

        return app;
    }
}