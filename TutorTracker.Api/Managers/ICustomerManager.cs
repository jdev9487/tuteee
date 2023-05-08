namespace TutorTracker.Api.Managers;

using M = Model;
using E = Entities;

public interface ICustomerManager
{
    Task<Guid?> CreateCustomerAsync(E.Customer customer, CancellationToken token);
    Task<E.Customer?> GetCustomerAsync(Guid customerId, CancellationToken token);
    Task<IEnumerable<E.Customer>> GetCustomersAsync(string? firstName, string? lastName, CancellationToken token);
    Task<IEnumerable<E.Lesson>> GetLessonsAssociatedWithCustomer(Guid customerId, int? month, int? year,
        CancellationToken token);

    Task<E.Customer?> UpdateCustomerAsync(M.UpdateCustomer customer, CancellationToken token);
}