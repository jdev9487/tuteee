using TutorTracker.Api.Entities;

namespace TutorTracker.Api.Repositories;

public class Repository : IRepository
{
    public bool SaveStudent(Student student) => student.FirstName.ToLower()[0] == 'a';
}