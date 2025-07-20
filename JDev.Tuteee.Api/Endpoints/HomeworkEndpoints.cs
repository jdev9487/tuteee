namespace JDev.Tuteee.Api.Endpoints;

using ApiClient.DTOs;
using DB;
using Mapping;

public class HomeworkEndpoints : IEndpoints
{
    public void MapRoutes(IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapPost("/homeworks",
            async (HomeworkDto dto, Context context, CancellationToken token) =>
            {
                var entity = HomeworkMap.Map(dto);
                await context.Homeworks.AddAsync(entity, token);
                await context.SaveChangesAsync(token);
                return TypedResults.Created();
            });
    }
}