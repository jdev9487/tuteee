namespace JDev.Tuteee.DAL.Entities;

using Core.EfCore;

public class HomeworkAttachment : BaseEntity
{
    public int HomeworkAttachmentId { get; set; }
    public string Path { get; set; }
    public int LessonId { get; set; }
    public virtual Lesson Lesson { get; set; } = default!;
}