namespace JDev.Tuteee.Api.Endpoints;

using DB;
using Entities;
using ApiClient;
using AutoMapper;
using ApiClient.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;

public class LessonEndpoints(IMapper mapper) : IEndpoints
{
    public void MapRoutes(IEndpointRouteBuilder routeBuilder)
    {
        var groupBuilder = routeBuilder.MapGroup($"/{Endpoint.LessonBase}");
        groupBuilder.MapGet("/{id:int}",
            async Task<Results<Ok<LessonDto>, NotFound>> (int id, Context context, CancellationToken token) =>
            {
                var lesson = await context.Lessons
                    .SingleOrDefaultAsync(l => l.LessonId == id, cancellationToken: token);
                return lesson is null ? TypedResults.NotFound() : TypedResults.Ok(mapper.Map<LessonDto>(lesson));
            });
        
        groupBuilder.MapPost("",
            async (LessonDto dto, Context context, CancellationToken token) =>
            {
                var entity = mapper.Map<Lesson>(dto);
                await context.Lessons.AddAsync(entity, token);
                await context.SaveChangesAsync(token);
                return TypedResults.Created();
            });
    }
}