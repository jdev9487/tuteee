namespace JDev.Tuteee.ApiClient.DTOs;

public class LessonDto
{
    public int? LessonId { get; set; }
    public DateTimeOffset StartTime { get; set; }
    public DateTimeOffset EndTime { get; set; }
    public int TuteeId { get; set; }
    public string? HomeworkInstructions { get; set; }
}