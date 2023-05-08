namespace TutorTracker.Api.Routing;

using Controllers;

internal static partial class WebApplicationExtensions
{
    internal static void MapCustomerEndpoints(this WebApplication app)
    {
        var customerController = app.Services.GetRequiredService<CustomerController>();
        
        app.MapGet("/customers/{customerId:guid}", customerController.GetCustomerAsync);
        app.MapGet("/customers", customerController.GetCustomersAsync);
        app.MapGet("/customers/{customerId:guid}/lessons", customerController.GetLessonsAssociatedWithCustomer);
        
        app.MapPost("/customers", customerController.CreateCustomerAsync);

        app.MapPatch("/customers", customerController.UpdateCustomerAsync);
    }
}