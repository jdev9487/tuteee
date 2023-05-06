namespace TutorTracker.Model;

public class CreateStudent
{
    public string FirstName { get; init; } = default!;
    public string LastName { get; init; } = default!;
    public Guid InvoiceeId { get; init; }
}