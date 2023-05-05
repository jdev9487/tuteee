namespace TutorTracker.Api.Routing;

using M = Model;
using Persistence.Repositories;
using E = Persistence.Entities;
using AutoMapper;

internal static partial class WebApplicationExtensions
{
    internal static void MapLessonEndpoints(this WebApplication app)
    {
        var repo = app.Services.GetRequiredService<IRepository>();
        var mapper = app.Services.GetRequiredService<IMapper>();

        app.MapPost("/lessons", async (M.Lesson lesson, CancellationToken token) =>
        {
            try
            {
                var studentTask = repo.GetStudentAsync(lesson.StudentId, token);
                var lessonEntity = mapper.Map<E.Lesson>(lesson);
                var student = await studentTask;
                if (student is null) return Results.BadRequest();
                lessonEntity.Student = student;
                return await repo.SaveLessonAsync(lessonEntity, token) ? Results.Ok() : Results.BadRequest();
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        });
    }
}