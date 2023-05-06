namespace TutorTracker.Model;

public class CustomerResult
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public IEnumerable<Guid> StudentIds { get; set; } = Array.Empty<Guid>();
}