namespace TutorTracker.Api.Managers;

using E = Entities;

public interface IStudentManager
{
    Task<Guid?> CreateStudentAsync(E.Student student, Guid invoiceeId, CancellationToken token);
}