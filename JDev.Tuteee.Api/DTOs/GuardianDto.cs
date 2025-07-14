namespace JDev.Tuteee.Api.DTOs;

public class GuardianDto
{
    public int GuardianId { get; set; }
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string EmailAddress { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public IEnumerable<int> TuteeIds { get; set; } = [];
}