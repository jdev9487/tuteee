namespace JDev.Tuteee.Api.Endpoints;

using DB;
using DTOs;
using Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;

public static class GuardianEndpoints
{
    public static void RegisterGuardianEndpoints(this WebApplication app)
    {
        app.MapGet("/guardians/{id:int}",
                async Task<Results<Ok<GuardianDto>, NotFound>> (int id, Context context, CancellationToken token) =>
                {
                    var guardian = await context.Guardians
                        .Include(t => t.Tutees)
                        .SingleOrDefaultAsync(g => g.GuardianId == id, cancellationToken: token);
                    return guardian is null ? TypedResults.NotFound() : TypedResults.Ok(GuardianMap.Map(guardian));
                })
            .WithOpenApi();

        app.MapPost("/guardians", async (GuardianDto dto, Context context, CancellationToken token) =>
            {
                var entity = GuardianMap.Map(dto);
                await context.Guardians.AddAsync(entity, token);
                await context.SaveChangesAsync(token);
                return TypedResults.Created();
            })
            .WithOpenApi();
    }
}