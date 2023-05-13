namespace TutorTracker.Model;

public class LessonResult
{
    public Guid StudentId { get; init; }
    public Guid Id { get; set; }
    public DateTimeOffset DateTime { get; init; }
    public decimal HourlyRate { get; init; }
    public int HalfHours { get; init; }
    public bool Paid { get; init; }
}