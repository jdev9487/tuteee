namespace JDev.Tuteee.Api.Entities;

public class Lesson
{
    public int LessonId { get; set; }
    public DateTimeOffset StartTime { get; set; }
    public DateTimeOffset EndTime { get; set; }
    public int TuteeId { get; set; }
    public virtual Tutee Tutee { get; set; } = default!;
    public int? InvoiceId { get; set; }
    public virtual Invoice? Invoice { get; set; }
    public string? HomeworkInstructions { get; set; }
}