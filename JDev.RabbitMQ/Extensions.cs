namespace JDev.RabbitMQ;

using MassTransit;
using Microsoft.Extensions.DependencyInjection;

public static class Extensions
{
    public static IServiceCollection AddRabbitMqPublisher(this IServiceCollection services, Auth auth)
    {
        return services.AddMassTransit(busConfigurator =>
        {
            busConfigurator.AddRabbitMq(auth);
        });
    }
    
    public static IServiceCollection AddRabbitMqConsumer<THandler>(this IServiceCollection services, Auth auth)
        where THandler : class, IConsumer
    {
        return services.AddMassTransit(busConfigurator =>
        {
            busConfigurator.AddConsumer<THandler>();
            busConfigurator.AddRabbitMq(auth);
        });
    }

    private static void AddRabbitMq(this IBusRegistrationConfigurator busConfigurator, Auth auth)
    {
        busConfigurator.SetKebabCaseEndpointNameFormatter();
        busConfigurator.UsingRabbitMq((context, config) =>
        {
            // config.Host("amqp://localhost:5672", h =>
            // {
            //     h.Username("guest");
            //     h.Password("guest");
            // });
            config.Host(auth.Host, h =>
            {
                h.Username(auth.Username);
                h.Password(auth.Password);
            });
        
            config.ConfigureEndpoints(context);
        });
    }
}