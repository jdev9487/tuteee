namespace TutorTracker.Persistence.Entities;

using System.Collections.ObjectModel;

public class Student
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    
    public Customer Invoicee { get; set; } = default!;
    public ICollection<Lesson> Lessons = new Collection<Lesson>();
}