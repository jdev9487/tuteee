namespace TutorTracker.Api.Routing;

using Controllers;
using M = Model;

internal static partial class WebApplicationExtensions
{
    internal static void MapStudentEndpoints(this WebApplication app)
    {
        var studentController = app.Services.GetRequiredService<StudentController>();

        app.MapGet("/students", studentController.GetStudentsAsync);
        app.MapGet("/students/{studentId:guid}", studentController.GetStudentAsync);

        app.MapPost("/students", studentController.CreateStudentAsync);
    }
}