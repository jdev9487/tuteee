namespace JDev.Tuteee.DAL.Entities;

public class HomeworkAttachment
{
    public int HomeworkAttachmentId { get; set; }
    public string Path { get; set; }
    public int LessonId { get; set; }
    public virtual Lesson Lesson { get; set; } = default!;
}