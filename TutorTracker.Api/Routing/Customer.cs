using Microsoft.AspNetCore.Http.HttpResults;
using TutorTracker.Api.Entities;
using TutorTracker.Api.Repositories;

namespace TutorTracker.Api.Routing;

using Parsing;
using M = Model;
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
                var customerEntity = mapper.Map<Customer>(customer);
                return await repo.SaveCustomerAsync(customerEntity, token)
                    ? Results.Ok(customerEntity.Id)
                    : Results.BadRequest();
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        });

        app.MapGet("/customers/{customerId:guid}", async (Guid customerId, CancellationToken token) =>
        {
            try
            {
                var customer = await repo.GetCustomerAsync(customerId, token);
                return customer is null ? Results.BadRequest() : Results.Ok(customer);
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        });

        app.MapGet("/customers", async (CancellationToken token) =>
        {
            try
            {
                return Results.Ok(await repo.GetCustomersAsync(token));
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