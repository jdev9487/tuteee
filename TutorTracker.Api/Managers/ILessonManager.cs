namespace TutorTracker.Api.Managers;

using E = Entities;

public interface ILessonManager
{
    Task<Guid?> CreateLessonAsync(E.Lesson lesson, Guid studentId, CancellationToken token);
}