namespace JDev.Tuteee.Api.Endpoints;

using DB;
using DTOs;
using Mapping;

public class LessonEndpoints : IEndpoints
{
    public void MapRoutes(IEndpointRouteBuilder routeBuilder)
    {
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