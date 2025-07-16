namespace JDev.Tuteee.Api.DTOs;

public class GuardianDto
{
    public int? GuardianId { get; set; }
    public required string FirstName { get; set; } = default!;
    public required string LastName { get; set; } = default!;
    public required string EmailAddress { get; set; } = default!;
    public required string PhoneNumber { get; set; } = default!;
    public IEnumerable<TuteeDto>? Tutees { get; set; } = [];
}