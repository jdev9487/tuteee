namespace JDev.Tuteee.Api.DTOs;

public class TuteeDto
{
    public int? TuteeId { get; set; }
    public required string FirstName { get; set; } = default!;
    public required string LastName { get; set; } = default!;
    public required string EmailAddress { get; set; } = default!;
    public required int GuardianId { get; set; }
    public IEnumerable<int>? LessonIds { get; set; } = [];
    public IEnumerable<int>? RateIds { get; set; } = [];
}