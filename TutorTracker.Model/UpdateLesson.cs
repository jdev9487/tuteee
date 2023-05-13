namespace TutorTracker.Model;

public class UpdateLesson
{
    public Guid LessonId { get; set; }
    public bool? Paid { get; set; }
    public DateTimeOffset? DateTime { get; set; }
    public int? HalfHours { get; set; }
    public decimal? HourlyRate { get; set; }
}