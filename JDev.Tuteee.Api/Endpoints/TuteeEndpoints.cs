namespace JDev.Tuteee.Api.Endpoints;

using DB;
using DTOs;
using Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;

public static class TuteeEndpoints
{
    public static void RegisterTuteeEndpoints(this WebApplication app)
    {
        app.MapGet("/tutees/{id:int}",
                async Task<Results<Ok<TuteeDto>, NotFound>> (int id, Context context, CancellationToken token) =>
                {
                    var entity = await context.Tutees
                        .Include(t => t.Guardian)
                        .SingleOrDefaultAsync(t => t.TuteeId == id, cancellationToken: token);
                    return entity is null ? TypedResults.NotFound() : TypedResults.Ok(TuteeMap.Map(entity));
                })
            .WithOpenApi();

        app.MapGet("/tutees", async (Context context, CancellationToken token) =>
            {
                var entities = await context.Tutees
                    .Include(t => t.Guardian)
                    .Include(t => t.Lessons)
                    .ToListAsync(cancellationToken: token);
                return TypedResults.Ok(entities.Select(TuteeMap.Map));
            })
            .WithOpenApi();

        app.MapPost("/tutees", async (TuteeDto dto, Context context, CancellationToken token) =>
            {
                var entity = TuteeMap.Map(dto);
                await context.Tutees.AddAsync(entity, token);
                await context.SaveChangesAsync(token);
                return TypedResults.Created();
            })
            .WithOpenApi();
    }
}