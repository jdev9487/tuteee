namespace TutorTracker.Model;

public class Lesson
{
    public Guid StudentId { get; init; }
    public DateTimeOffset DateTime { get; init; }
    public decimal HourlyRate { get; init; }
    public int HalfHours { get; init; }
    public bool Paid { get; init; }
}