namespace TutorTracker.Model;

public class StudentResult
{
    public string FirstName { get; init; } = default!;
    public string LastName { get; init; } = default!;
    public Guid InvoiceeId { get; init; }
    public IEnumerable<Guid> LessonIds { get; set; } = Array.Empty<Guid>();
}