using TutorTracker.Api.Entities;

namespace TutorTracker.Api.Repositories;

public interface IRepository
{
    bool SaveStudent(Student student);
}