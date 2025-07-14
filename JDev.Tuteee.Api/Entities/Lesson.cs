namespace JDev.Tuteee.Api.Entities;

public class Lesson
{
    public virtual int LessonId { get; set; }
    public virtual int StartTime { get; set; }
    public virtual int EndTime { get; set; }
    public virtual int TuteeId { get; set; }
    public virtual Tutee Tutee { get; set; } = default!;
}