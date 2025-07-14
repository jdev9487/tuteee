namespace JDev.Tuteee.Api.DTOs;

public class TuteeDto
{
    public int TuteeId { get; set; }
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string EmailAddress { get; set; } = default!;
    public int GuardianId { get; set; }
    public IEnumerable<int> LessonIds { get; set; } = [];
    public IEnumerable<int> RateIds { get; set; } = [];
}