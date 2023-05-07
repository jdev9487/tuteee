namespace TutorTracker.Api.Entities;

using System.Collections.ObjectModel;

public class Customer
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Phone { get; set; } = default!;

    public ICollection<Student> Students { get; set; } = new Collection<Student>();
}