namespace TutorTracker.Api.Routing;

using Parsing;
using Persistence.Repositories;
using M = Model;
using E = Persistence.Entities;
using AutoMapper;

internal static partial class WebApplicationExtensions
{
    internal static void MapCustomerEndpoints(this WebApplication app)
    {
        var repo = app.Services.GetRequiredService<IRepository>();
        var mapper = app.Services.GetRequiredService<IMapper>();
        var dateParser = app.Services.GetRequiredService<IDateParser>();

        app.MapPost("/customers", async (M.Customer customer, CancellationToken token) =>
        {
            try
            {
                return await repo.SaveCustomerAsync(mapper.Map<E.Customer>(customer), token)
                    ? Results.Ok()
                    : Results.BadRequest();
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        });
        
        app.MapGet("customers/{customerId:guid}/lessons/",
            async (Guid customerId, int? month, int? year, CancellationToken token) =>
        {
            try
            {
                var period = dateParser.GetPeriod(month, year);
                var lessons =
                    await repo.GetLessonsAssociatedWithCustomerAsync(customerId, period.Start, period.End, token);
                return Results.Ok(lessons);
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        });
    }
}