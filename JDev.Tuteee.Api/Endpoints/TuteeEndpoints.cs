namespace JDev.Tuteee.Api.Endpoints;

using ApiClient.DTOs;
using DB;
using Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;

public class TuteeEndpoints : IEndpoints
{
    public void MapRoutes(IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapGet("/tutees/{id:int}",
            async Task<Results<Ok<TuteeDto>, NotFound>> (int id, Context context, CancellationToken token) =>
            {
                var entity = await context.Tutees
                    .Include(t => t.Client)
                    .Include(t => t.Lessons)
                    .SingleOrDefaultAsync(t => t.TuteeId == id, cancellationToken: token);
                return entity is null ? TypedResults.NotFound() : TypedResults.Ok(TuteeMap.Map(entity));
            });

        routeBuilder.MapGet("/tutees",
            async (Context context, CancellationToken token) =>
            {
                var entities = await context.Tutees
                    .Include(t => t.Client)
                    .Include(t => t.Lessons)
                    .ToListAsync(cancellationToken: token);
                return TypedResults.Ok(entities.Select(TuteeMap.Map));
            });

        routeBuilder.MapPost("/tutees",
            async (TuteeDto dto, Context context, CancellationToken token) =>
            {
                var entity = TuteeMap.Map(dto);
                await context.Tutees.AddAsync(entity, token);
                await context.SaveChangesAsync(token);
                return TypedResults.Created();
            });
    }
}