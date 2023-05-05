namespace TutorTracker.Persistence.Repositories;

using Entities;

public interface IRepository
{
    Task<bool> SaveStudentAsync(Student student, CancellationToken token);
    Task<bool> SaveCustomerAsync(Customer customer, CancellationToken token);
    Task<bool> SaveLessonAsync(Lesson lesson, CancellationToken token);
    Task<Customer?> GetCustomerAsync(Guid id, CancellationToken token);
    Task<Student?> GetStudentAsync(Guid id, CancellationToken token);
    Task<IEnumerable<Lesson>> GetLessonsAssociatedWithCustomerAsync(Guid customerId, DateTimeOffset? from,
        DateTimeOffset? to, CancellationToken token);
}