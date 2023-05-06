namespace TutorTracker.Api.Managers;

using Entities;
using Repositories;

public class LessonManager : ILessonManager
{
    private readonly IRepository _repository;
    private readonly ILogger<LessonManager> _logger;

    public LessonManager(IRepository repository, ILogger<LessonManager> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Guid?> CreateLessonAsync(Lesson lesson, Guid studentId, CancellationToken token)
    {
        try
        {
            var student = await _repository.GetStudentAsync(studentId, token);
            if (student is null) throw new KeyNotFoundException($"Student with id {studentId} does not exist");
            lesson.Student = student;
            return await _repository.SaveLessonAsync(lesson, token) ? lesson.Id : null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Could not create lesson");
            throw;
        }
    }
}