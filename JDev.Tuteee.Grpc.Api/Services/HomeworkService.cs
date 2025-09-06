namespace JDev.Tuteee.Grpc.Api.Services;

using Protos;
using Messages;
using MassTransit;
using DAL.Entities;
using DAL.Repository;
using global::Grpc.Core;
using Razor.Templating.Core;

public class HomeworkService(
    IGenericRepository repository,
    IRazorTemplateEngine razorTemplateEngine,
    IBus bus) : Homework.HomeworkBase
{
    public override async Task<ReleaseResponse> Release(ReleaseRequest request, ServerCallContext context)
    {
        var lesson = await repository.FindAsync<Lesson>(request.LessonId, context.CancellationToken);
        if (lesson is null)
            return new ReleaseResponse
            {
                Success = false,
                Message = $"Could not find lesson with id {request.LessonId}"
            };

        var htmlTask = razorTemplateEngine.RenderAsync("EmailTemplates/Homework.cshtml", new EmailTemplates.Homework
        {
            FirstName = lesson.Tutee.FirstName,
            Instructions = lesson.HomeworkInstructions
        });
        
        _ = bus.Publish(new EmailHomeworkEvent
        {
            To = lesson.Tutee.EmailAddress,
            Body = await htmlTask,
            Date = lesson.StartTime.ToString("D")
        }, context.CancellationToken);

        return new ReleaseResponse { Success = true };
    }
}