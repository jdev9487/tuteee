namespace TutorTracker.Model;

public class UpdateCustomer
{
    public Guid Id { get; set; }
    public string? FirstName { get; set; } = default!;
    public string? LastName { get; set; } = default!;
    public string? Email { get; set; } = default!;
    public string? Phone { get; set; } = default!;
}