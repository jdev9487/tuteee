namespace TutorTracker.Api.Repositories;

using Entities;
using System.Linq.Expressions;

public interface IRepository
{
    Task<bool> SaveStudentAsync(Student student, CancellationToken token);
    Task<bool> SaveCustomerAsync(Customer customer, CancellationToken token);
    Task<bool> SaveLessonAsync(Lesson lesson, CancellationToken token);
    Task<IEnumerable<Customer>> GetCustomersAsync(CancellationToken token);
    Task<IEnumerable<Customer>> GetCustomersAsync(Expression<Func<Customer, bool>> predicate, CancellationToken token);
    Task<Customer?> GetCustomerAsync(Guid id, CancellationToken token);
    Task<Student?> GetStudentAsync(Guid id, CancellationToken token);
    Task<IEnumerable<Lesson>> GetLessonsAssociatedWithCustomerAsync(Guid customerId, DateTimeOffset? from,
        DateTimeOffset? to, CancellationToken token);
}