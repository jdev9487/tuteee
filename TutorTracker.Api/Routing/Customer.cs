using TutorTracker.Api.Parsing;

namespace TutorTracker.Api.Routing;

using M = Model;
using E = Entities;
using AutoMapper;
using Repositories;

internal static partial class WebApplicationExtensions
{
    internal static void MapCustomerEndpoints(this WebApplication app)
    {
        var repo = app.Services.GetRequiredService<IRepository>();
        var mapper = app.Services.GetRequiredService<IMapper>();
        var monthParser = app.Services.GetRequiredService<IDateParser>();

        app.MapPost("/customers", (M.Customer customer) =>
        {
            try
            {
                return repo.SaveCustomer(mapper.Map<E.Customer>(customer)) ? Results.Ok() : Results.BadRequest();
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        });
        
        app.MapGet("customers/{customerId:guid}/lessons/", (Guid customerId, int? month, int? year) =>
        {
            try
            {
                var period = monthParser.GetPeriod(month, year);
                var lessons = repo.GetLessonsAssociatedWithCustomer(customerId, period.Start, period.End);
                return Results.Ok(lessons);
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        });
    }
}