namespace TutorTracker.Api.Repositories;

using Context;
using Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

public class Repository : IRepository
{
    private readonly ApplicationContext _applicationContext; 
    public Repository(ApplicationContext applicationContext)
    {
        _applicationContext = applicationContext;
    }
    
    public async Task<bool> SaveStudentAsync(Student student, CancellationToken token)
    {
        _applicationContext.Students.Add(student);
        return await _applicationContext.SaveChangesAsync(token) > 0;
    }

    public async Task<bool> SaveCustomerAsync(Customer customer, CancellationToken token)
    {
        _applicationContext.Customers.Add(customer);
        return await _applicationContext.SaveChangesAsync(token) > 0;
    }

    public async Task<bool> SaveLessonAsync(Lesson lesson, CancellationToken token)
    {
        _applicationContext.Lessons.Add(lesson);
        return await _applicationContext.SaveChangesAsync(token) > 0;
    }

    public async Task<IEnumerable<Customer>> GetCustomersAsync(CancellationToken token) =>
        await _applicationContext.Customers.ToArrayAsync(cancellationToken: token);

    public async Task<IEnumerable<Customer>> GetCustomersAsync(string firstName, string lastName,
        CancellationToken token)
    {
        return await _applicationContext.Customers
            .Where(x => string.Equals(x.FirstName.ToLower(), firstName.ToLower(), StringComparison.OrdinalIgnoreCase))
            .Where(x => string.Equals(x.LastName.ToLower(), lastName.ToLower(), StringComparison.OrdinalIgnoreCase))
            .ToArrayAsync(token);
    }
    public async Task<IEnumerable<Customer>> GetCustomersAsync(Expression<Func<Customer, bool>> predicate,
        CancellationToken token)
    {
        return await _applicationContext.Customers
            .Where(predicate).ToArrayAsync(token);
    }

    public async Task<Customer?> GetCustomerAsync(Guid id, CancellationToken token) =>
        await _applicationContext.Customers.FindAsync(new object?[] { id }, token);

    public async Task<Student?> GetStudentAsync(Guid id, CancellationToken token) =>
        await _applicationContext.Students.FindAsync(new object?[] { id }, token);

    public async Task<IEnumerable<Lesson>> GetLessonsAssociatedWithCustomerAsync(Guid customerId, DateTimeOffset? from,
        DateTimeOffset? to, CancellationToken token) =>
        await _applicationContext.Lessons.Include(l => l.Student).ThenInclude(s => s.Invoicee)
            .Where(l => l.Student.Invoicee.Id == customerId)
            .Where(l => l.DateTime > from)
            .Where(l => l.DateTime < to).ToArrayAsync(token);
}