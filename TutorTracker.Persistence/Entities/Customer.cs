namespace TutorTracker.Persistence.Entities;

using System.Collections.ObjectModel;

public class Customer
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;

    public ICollection<Student> Students { get; set; } = new Collection<Student>();
}