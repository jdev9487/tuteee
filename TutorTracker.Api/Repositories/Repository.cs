using TutorTracker.Api.Entities;

namespace TutorTracker.Api.Repositories;

public class Repository : IRepository
{
    public bool SaveStudent(Student student) => throw new NotImplementedException();
    public bool SaveCustomer(Customer customer) => throw new NotImplementedException();
    public bool SaveLesson(Lesson lesson) => throw new NotImplementedException();
    public Customer GetCustomer(Guid id) => throw new NotImplementedException();
    public Student GetStudent(Guid id) => throw new NotImplementedException();
    public IEnumerable<Lesson> GetLessonsAssociatedWithCustomer(Guid customerId, DateTimeOffset? from,
        DateTimeOffset? to)
    {
        throw new NotImplementedException();
    }
}