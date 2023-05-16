using TutorTracker.Model;

namespace TutorTracker.Api.Repositories;

using Context;
using Entities;
using Microsoft.EntityFrameworkCore;

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

    public async Task<Customer?> UpdateCustomerAsync(UpdateCustomer customer, CancellationToken token)
    {
        var existingCustomer = await GetCustomerAsync(customer.Id, token);
        if (existingCustomer is not null)
        {
            existingCustomer.FirstName = customer.FirstName ?? existingCustomer.FirstName;
            existingCustomer.LastName = customer.LastName ?? existingCustomer.LastName;
            existingCustomer.Phone = customer.Phone ?? existingCustomer.Phone;
            existingCustomer.Email = customer.Email ?? existingCustomer.Email;
        }
        return await _applicationContext.SaveChangesAsync(token) > 0 ? existingCustomer : null;
    }

    public async Task<bool> SaveLessonAsync(Lesson lesson, CancellationToken token)
    {
        _applicationContext.Lessons.Add(lesson);
        return await _applicationContext.SaveChangesAsync(token) > 0;
    }

    public async Task<IEnumerable<Customer>> GetCustomersAsync(string? firstName, string? lastName,
        CancellationToken token)
    {
        var customers = _applicationContext.Customers.Include(x => x.Students).AsQueryable();
        if (firstName is not null)
        {
            customers = customers.Where(x => string.Equals(x.FirstName.ToLower(), firstName.ToLower()));
        }
        if (lastName is not null)
        {
            customers = customers.Where(x => string.Equals(x.LastName.ToLower(), lastName.ToLower()));
        }
        return await customers.ToArrayAsync(token);
    }

    public async Task<Customer?> GetCustomerAsync(Guid id, CancellationToken token) =>
        await _applicationContext.Customers.Include(x => x.Students).FirstOrDefaultAsync(x => x.Id == id, token);

    public async Task<Student?> GetStudentAsync(Guid id, CancellationToken token) =>
        await _applicationContext.Students.Include(x => x.Lessons).Include(x => x.Invoicee)
            .FirstOrDefaultAsync(x => x.Id == id, token);

    public async Task<IEnumerable<Lesson>> GetLessonsAssociatedWithCustomerAsync(Guid customerId, DateTimeOffset? from,
        DateTimeOffset? to, CancellationToken token) =>
        await _applicationContext.Lessons.Include(l => l.Student).ThenInclude(s => s.Invoicee)
            .Where(l => l.Student.Invoicee.Id == customerId)
            .Where(l => l.DateTime > from)
            .Where(l => l.DateTime < to).ToArrayAsync(token);

    public async Task<IEnumerable<Student>> GetStudentsAsync(CancellationToken token) => await _applicationContext
        .Students.Include(x => x.Lessons).Include(x => x.Invoicee).ToArrayAsync(token);

    public async Task<IEnumerable<Lesson>> GetLessonsAssociatedWithStudentAsync(Guid studentId, CancellationToken token)
        => await _applicationContext.Lessons.Include(x => x.Student)
            .Where(x => x.Student.Id == studentId)
            .OrderByDescending(x => x.DateTime)
            .ToArrayAsync(token);

    public async Task<Lesson?> UpdateLessonAsync(UpdateLesson updateLesson, CancellationToken token)
    {
        var existingLesson = await GetLessonAsync(updateLesson.LessonId, token);
        if (existingLesson is not null)
        {
            existingLesson.DateTime = updateLesson.DateTime ?? existingLesson.DateTime;
            existingLesson.Paid = updateLesson.Paid ?? existingLesson.Paid;
            existingLesson.HalfHours = updateLesson.HalfHours ?? existingLesson.HalfHours;
            existingLesson.HourlyRate = updateLesson.HourlyRate ?? existingLesson.HourlyRate;
        }
        return await _applicationContext.SaveChangesAsync(token) > 0 ? existingLesson : null;
    }

    public async Task<Lesson?> DeleteLessonAsync(Guid lessonId, CancellationToken token)
    {
        var lesson = await _applicationContext.Lessons.FindAsync(new object?[] { lessonId }, cancellationToken: token);
        if (lesson is null) return null;
        _applicationContext.Lessons.Remove(lesson);
        await _applicationContext.SaveChangesAsync(token);
        return lesson;
    }

    private async Task<Lesson?> GetLessonAsync(Guid id, CancellationToken token) =>
        await _applicationContext.Lessons.Include(x => x.Student).FirstOrDefaultAsync(x => x.Id == id, token);
}