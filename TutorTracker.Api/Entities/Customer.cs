using System.Collections.ObjectModel;

namespace TutorTracker.Api.Entities;

public class Customer
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;

    public ICollection<Student> Students { get; set; } = new Collection<Student>();
}