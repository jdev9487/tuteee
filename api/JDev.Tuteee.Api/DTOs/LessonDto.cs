namespace JDev.Tuteee.Api.DTOs;

public class LessonDto
{
    public int? LessonId { get; set; }
    public int StartTime { get; set; }
    public int EndTime { get; set; }
    public int TuteeId { get; set; }
    public int? HomeworkId { get; set; } = default!;
}