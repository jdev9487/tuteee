namespace JDev.Tuteee.Api.Entities;

public class Lesson
{
    public int LessonId { get; set; }
    public int StartTime { get; set; }
    public int EndTime { get; set; }
    public int TuteeId { get; set; }
    public Tutee Tutee { get; set; } = default!;
    public Homework? Homework { get; set; } = default!;
}