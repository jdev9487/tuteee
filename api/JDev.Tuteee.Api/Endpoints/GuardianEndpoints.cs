namespace JDev.Tuteee.Api.Endpoints;

using DB;
using DTOs;
using Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;

public class GuardianEndpoints : IEndpoints
{
    public void MapRoutes(IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapGet("/guardians/{id:int}",
            async Task<Results<Ok<GuardianDto>, NotFound>> (int id, Context context, CancellationToken token) =>
            {
                var guardian = await context.Guardians
                    .Include(t => t.Tutees)
                    .SingleOrDefaultAsync(g => g.GuardianId == id, cancellationToken: token);
                return guardian is null ? TypedResults.NotFound() : TypedResults.Ok(GuardianMap.Map(guardian));
            });
        
        routeBuilder.MapGet("/guardians",
            async (Context context, CancellationToken token) =>
            {
                var entities = await context.Guardians
                    .Include(g => g.Tutees)
                    .ToListAsync(cancellationToken: token);
                return TypedResults.Ok(entities.Select(GuardianMap.Map));
            });

        routeBuilder.MapPost("/guardians",
            async (GuardianDto dto, Context context, CancellationToken token) =>
            {
                var entity = GuardianMap.Map(dto);
                await context.Guardians.AddAsync(entity, token);
                await context.SaveChangesAsync(token);
                return TypedResults.Created();
            });
    }
}