namespace JDev.Tuteee.Api.Entities;

public class Homework
{
    public int HomeworkId { get; set; }
    public string Instructions { get; set; } = string.Empty;
    public int LessonId { get; set; }
    public Lesson Lesson { get; set; } = default!;
    public IList<HomeworkAttachment> HomeworkAttachments { get; set; } = [];
}