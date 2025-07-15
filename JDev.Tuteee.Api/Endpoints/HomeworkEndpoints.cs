namespace JDev.Tuteee.Api.Endpoints;

using DB;
using DTOs;
using Mapping;

public static class HomeworkEndpoints
{
    public static void RegisterHomeworkEndpoints(this WebApplication app)
    {
        app.MapPost("/homeworks", async (HomeworkDto dto, Context context, CancellationToken token) =>
            {
                var entity = HomeworkMap.Map(dto);
                await context.Homeworks.AddAsync(entity, token);
                await context.SaveChangesAsync(token);
                return TypedResults.Created();
            })
            .WithOpenApi();
    }
}