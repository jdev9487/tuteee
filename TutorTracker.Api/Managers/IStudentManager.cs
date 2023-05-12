namespace TutorTracker.Api.Managers;

using E = Entities;

public interface IStudentManager
{
    Task<IEnumerable<E.Student>> GetStudentsAsync(CancellationToken token);
    Task<Guid?> CreateStudentAsync(E.Student student, Guid invoiceeId, CancellationToken token);
    Task<E.Student?> GetStudentAsync(Guid studentId, CancellationToken token);
    Task<IEnumerable<E.Lesson>> GetLessonsAssociatedWithStudent(Guid studentId, CancellationToken token);
}