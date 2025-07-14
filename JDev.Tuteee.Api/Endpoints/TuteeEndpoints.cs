namespace JDev.Tuteee.Api.Endpoints;

using DB;
using DTOs;
using Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;

public static class TuteeEndpoints
{
    public static RouteHandlerBuilder RegisterTuteeEndpoints(this WebApplication app)
    {
        return app.MapGet("/tutees/{id:int}", Results<Ok<TuteeDto>, NotFound> (int id, Context context) =>
            {
                var tutee = context.Tutees.Include(t => t.Guardian).SingleOrDefault(t => t.TuteeId == id);
                return tutee is null ? TypedResults.NotFound() : TypedResults.Ok(TuteeMap.Map(tutee));
            })
            .WithOpenApi();
    }
}