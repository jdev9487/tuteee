using TutorTracker.Model;

namespace TutorTracker.Api.Managers;

using E = Entities;

public interface ILessonManager
{
    Task<Guid?> CreateLessonAsync(E.Lesson lesson, Guid studentId, CancellationToken token);
    Task<E.Lesson?> UpdateLessonAsync(UpdateLesson updateLesson, CancellationToken token);
    Task<E.Lesson?> DeleteLessonAsync(Guid lessonId, CancellationToken token);
}