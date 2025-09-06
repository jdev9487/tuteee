namespace JDev.RabbitMQ;

using MassTransit;
using Microsoft.Extensions.DependencyInjection;

public static class Extensions
{
    public static IServiceCollection AddRabbitMqPublisher(this IServiceCollection services)
    {
        return services.AddMassTransit(busConfigurator =>
        {
            busConfigurator.AddRabbitMq();
        });
    }
    
    public static IServiceCollection AddRabbitMqConsumer<THandler>(this IServiceCollection services)
        where THandler : class, IConsumer
    {
        return services.AddMassTransit(busConfigurator =>
        {
            busConfigurator.AddConsumer<THandler>();
            busConfigurator.AddRabbitMq();
        });
    }

    private static void AddRabbitMq(this IBusRegistrationConfigurator busConfigurator)
    {
        busConfigurator.SetKebabCaseEndpointNameFormatter();
        busConfigurator.UsingRabbitMq((context, config) =>
        {
            config.Host("amqp://localhost:5672", h =>
            {
                h.Username("guest");
                h.Password("guest");
            });
        
            config.ConfigureEndpoints(context);
        });
    }
}