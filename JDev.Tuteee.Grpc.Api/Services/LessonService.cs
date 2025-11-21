namespace JDev.Tuteee.Grpc.Api.Services;

using Core.EfCore.Repository;
using Messages;
using MassTransit;
using global::Grpc.Core;
using Protos;
using Lesson = DAL.Entities.Lesson;

public class LessonService(
    IGenericRepository repository,
    IBus bus) : Protos.Lesson.LessonBase
{
    public override async Task<ReleaseSummaryResponse> ReleaseSummary(ReleaseSummaryRequest request, ServerCallContext context)
    {
        var lesson = await repository.FindAsync<Lesson>(request.LessonId, context.CancellationToken);
        if (lesson is null)
            return new ReleaseSummaryResponse
            {
                Success = false,
                Message = $"Could not find lesson with id {request.LessonId}"
            };
        if (lesson.EmailSent)
            return new ReleaseSummaryResponse
            {
                Success = false,
                Message = $"Email already sent for lesson with id {request.LessonId}"
            };

        _ = bus.Publish(new LessonSummaryEvent
        {
            LessonId = lesson.LessonId,
        }, context.CancellationToken);

        lesson.EmailSent = true;
        await repository.SaveChangesAsync(context.CancellationToken);

        return new ReleaseSummaryResponse { Success = true };
    }
}