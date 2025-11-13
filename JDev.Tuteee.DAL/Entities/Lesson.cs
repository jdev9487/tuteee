namespace JDev.Tuteee.DAL.Entities;

using Core.EfCore;

public class Lesson : BaseEntity
{
    public int LessonId { get; set; }
    public DateTimeOffset StartTime { get; set; }
    public DateTimeOffset EndTime { get; set; }
    public int TuteeId { get; set; }
    public virtual TuteeRole TuteeRole { get; set; } = default!;
    public int? InvoiceId { get; set; }
    public virtual Invoice? Invoice { get; set; }
    public string? HomeworkInstructions { get; set; }
    public virtual IList<HomeworkAttachment> HomeworkAttachments { get; set; } = [];
}