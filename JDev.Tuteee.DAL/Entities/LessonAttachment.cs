namespace JDev.Tuteee.DAL.Entities;

using Core.EfCore;

public class LessonAttachment : BaseEntity
{
    public int LessonAttachmentId { get; set; }
    public string Path { get; set; }
    public int LessonId { get; set; }
    public virtual Lesson Lesson { get; set; } = default!;
}