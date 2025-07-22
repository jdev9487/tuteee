namespace JDev.Tuteee.Api.Endpoints;

using ApiClient.DTOs;
using DB;
using Mapping;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

public class LessonEndpoints : IEndpoints
{
    public void MapRoutes(IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapGet("/lessons/{id:int}",
            async Task<Results<Ok<LessonDto>, NotFound>> (int id, Context context, CancellationToken token) =>
            {
                var lesson = await context.Lessons
                    .SingleOrDefaultAsync(l => l.LessonId == id, cancellationToken: token);
                return lesson is null ? TypedResults.NotFound() : TypedResults.Ok(LessonMap.Map(lesson));
            });
        
        routeBuilder.MapPost("/lessons",
            async (LessonDto dto, Context context, CancellationToken token) =>
            {
                var entity = LessonMap.Map(dto);
                await context.Lessons.AddAsync(entity, token);
                await context.SaveChangesAsync(token);
                return TypedResults.Created();
            });
    }
}