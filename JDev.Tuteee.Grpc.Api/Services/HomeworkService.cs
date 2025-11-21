namespace JDev.Tuteee.Grpc.Api.Services;

using Core.EfCore.Repository;
using Protos;
using Messages;
using MassTransit;
using DAL.Entities;
using global::Grpc.Core;

public class HomeworkService(
    IGenericRepository repository,
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
        if (lesson.EmailSent)
            return new ReleaseResponse
            {
                Success = false,
                Message = $"Email already sent for lesson with id {request.LessonId}"
            };

        _ = bus.Publish(new EmailHomeworkEvent
        {
            LessonId = lesson.LessonId,
        }, context.CancellationToken);

        lesson.EmailSent = true;
        await repository.SaveChangesAsync(context.CancellationToken);

        return new ReleaseResponse { Success = true };
    }
}