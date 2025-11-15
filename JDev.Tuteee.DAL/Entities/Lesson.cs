namespace JDev.Tuteee.DAL.Entities;

using Core.EfCore;

public class Lesson : BaseEntity
{
    public int LessonId { get; set; }
    public DateOnly Date { get; set; }
    public TimeOnly Start { get; set; }
    public TimeSpan Duration { get; set; }
    public DateTimeOffset StartTime { get; set; }
    public DateTimeOffset EndTime { get; set; }
    public bool EmailSent { get; set; } = false;
    public int TuteeRoleId { get; set; }
    public virtual TuteeRole TuteeRole { get; set; } = default!;
    public int? InvoiceId { get; set; }
    public virtual Invoice? Invoice { get; set; }
    public string? HomeworkInstructions { get; set; }
    public virtual IList<HomeworkAttachment> HomeworkAttachments { get; set; } = [];
}