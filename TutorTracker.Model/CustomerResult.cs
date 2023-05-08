namespace TutorTracker.Model;

public class CustomerResult
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Phone { get; set; } = default!;
    
    public IEnumerable<CustomerStudentResult> Students { get; set; } = Array.Empty<CustomerStudentResult>();
}

public class CustomerStudentResult
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public Guid Id { get; set; }
}