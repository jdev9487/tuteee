namespace TutorTracker.Api.Managers;

using Repositories;
using E = Entities;

public class StudentManager : IStudentManager
{
    private readonly IRepository _repository;
    private readonly ILogger<StudentManager> _logger;

    public StudentManager(IRepository repository, ILogger<StudentManager> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<IEnumerable<E.Student>> GetStudentsAsync(CancellationToken token)
    {
        try
        {
            var students = await _repository.GetStudentsAsync(token);
            return students;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Could not get students");
            throw;
        }
    }

    public async Task<Guid?> CreateStudentAsync(E.Student student, Guid invoiceeId, CancellationToken token)
    {
        try
        {
            var invoicee = await _repository.GetCustomerAsync(invoiceeId, token);
            if (invoicee is null) throw new KeyNotFoundException($"Invoicee with id {invoiceeId} does not exist");
            student.Invoicee = invoicee;
            invoicee.Students.Add(student);
            return await _repository.SaveStudentAsync(student, token) ? student.Id : null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Could not create student");
            throw;
        }
    }

    public async Task<E.Student?> GetStudentAsync(Guid studentId, CancellationToken token)
    {
        try
        {
            return await _repository.GetStudentAsync(studentId, token);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Could not get student with id {id}", studentId);
            throw;
        }
    }

    public async Task<IEnumerable<E.Lesson>> GetLessonsAssociatedWithStudent(Guid studentId, CancellationToken token)
    {
        try
        {
            return await _repository.GetLessonsAssociatedWithStudentAsync(studentId, token);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Could not get lessons for student with id {id}", studentId);
            throw;
        }
    }
}