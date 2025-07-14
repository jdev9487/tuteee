namespace JDev.Tuteee.Api.Endpoints;

using DB;
using DTOs;
using Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;

public static class GuardianEndpoints
{
    public static RouteHandlerBuilder RegisterGuardianEndpoints(this WebApplication app)
    {
        return app.MapGet("/guardians/{id:int}", Results<Ok<GuardianDto>, NotFound>(int id, Context context) =>
            {
                var guardian = context.Guardians.Include(t => t.Tutees).SingleOrDefault(g => g.GuardianId == id);
                return guardian is null ? TypedResults.NotFound() : TypedResults.Ok(GuardianMap.Map(guardian));
            })
            .WithOpenApi();
    }
}