namespace JDev.Tuteee.Api.Entities;

public class HomeworkAttachment
{
    public int HomeworkAttachmentId { get; set; }
    public string FileName { get; set; } = string.Empty;
    public int HomeworkId { get; set; }
    public Homework Homework { get; set; } = default!;
}