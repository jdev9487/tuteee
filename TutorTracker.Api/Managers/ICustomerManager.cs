using System.Linq.Expressions;

namespace TutorTracker.Api.Managers;

using E = Entities;

public interface ICustomerManager
{
    Task<Guid?> CreateCustomerAsync(E.Customer customer, CancellationToken token);
    Task<E.Customer?> GetCustomerAsync(Guid customerId, CancellationToken token);
    Task<IEnumerable<E.Customer>> GetCustomersAsync(CancellationToken token);
    Task<IEnumerable<E.Customer>> GetCustomersAsync(Expression<Func<E.Customer, bool>> predicate, CancellationToken token);
    Task<IEnumerable<E.Lesson>> GetLessonsAssociatedWithCustomer(Guid customerId, int? month, int? year,
        CancellationToken token);
}