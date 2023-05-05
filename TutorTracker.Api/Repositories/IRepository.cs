using TutorTracker.Api.Entities;

namespace TutorTracker.Api.Repositories;

public interface IRepository
{
    bool SaveStudent(Student student);
    bool SaveCustomer(Customer customer);
    bool SaveLesson(Lesson lesson);
    Customer GetCustomer(Guid id);
    Student GetStudent(Guid id);
    IEnumerable<Lesson> GetLessonsAssociatedWithCustomer(Guid customerId, DateTimeOffset? from, DateTimeOffset? to);
}