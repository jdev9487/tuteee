namespace TutorTracker.Persistence.Repositories;

using Microsoft.EntityFrameworkCore;
using Entities;

public class Repository : IRepository
{
    private readonly Context.Context _context; 
    public Repository(Context.Context context)
    {
        _context = context;
    }
    
    public async Task<bool> SaveStudentAsync(Student student, CancellationToken token)
    {
        _context.Students.Add(student);
        return await _context.SaveChangesAsync(token) > 0;
    }

    public async Task<bool> SaveCustomerAsync(Customer customer, CancellationToken token)
    {
        _context.Customers.Add(customer);
        return await _context.SaveChangesAsync(token) > 0;
    }

    public async Task<bool> SaveLessonAsync(Lesson lesson, CancellationToken token)
    {
        _context.Lessons.Add(lesson);
        return await _context.SaveChangesAsync(token) > 0;
    }

    public async Task<Customer?> GetCustomerAsync(Guid id, CancellationToken token) =>
        await _context.Customers.FindAsync(new object?[] { id }, token);

    public async Task<Student?> GetStudentAsync(Guid id, CancellationToken token) =>
        await _context.Students.FindAsync(new object?[] { id }, token);

    public async Task<IEnumerable<Lesson>> GetLessonsAssociatedWithCustomerAsync(Guid customerId, DateTimeOffset? from,
        DateTimeOffset? to, CancellationToken token) =>
        await _context.Lessons.Include(l => l.Student).ThenInclude(s => s.Invoicee)
            .Where(l => l.Student.Invoicee.Id == customerId)
            .Where(l => l.DateTime > from)
            .Where(l => l.DateTime < to).ToArrayAsync(token);
}