namespace TutorTracker.Persistence.Entities;

public class Lesson
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public DateTimeOffset DateTime { get; set; }
    public decimal HourlyRate { get; set; }
    public int HalfHours { get; set; }
    public bool Paid { get; set; }

    public Student Student { get; set; } = default!;
}