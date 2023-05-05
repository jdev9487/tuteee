namespace TutorTracker.Api.Routing;

using M = Model;
using E = Entities;
using AutoMapper;
using Repositories;

internal static partial class WebApplicationExtensions
{
    internal static void MapLessonEndpoints(this WebApplication app)
    {
        var repo = app.Services.GetRequiredService<IRepository>();
        var mapper = app.Services.GetRequiredService<IMapper>();

        app.MapPost("/lessons", (M.Lesson lesson) =>
        {
            try
            {
                var lessonEntity = mapper.Map<E.Lesson>(lesson);
                var student = repo.GetStudent(lesson.StudentId);
                lessonEntity.Student = student;
                return repo.SaveLesson(lessonEntity) ? Results.Ok() : Results.BadRequest();
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        });
    }
}