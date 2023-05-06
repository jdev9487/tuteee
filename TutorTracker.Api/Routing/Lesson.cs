namespace TutorTracker.Api.Routing;

using M = Model;
using Controllers;

internal static partial class WebApplicationExtensions
{
    internal static void MapLessonEndpoints(this WebApplication app)
    {
        var lessonController = app.Services.GetRequiredService<LessonController>();

        app.MapPost("/lessons", lessonController.CreateLessonAsync);
    }
}