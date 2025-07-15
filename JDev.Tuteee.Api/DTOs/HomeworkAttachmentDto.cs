namespace JDev.Tuteee.Api.DTOs;

public class HomeworkAttachmentDto
{
    public int? HomeworkAttachmentId { get; set; }
    public string FileName { get; set; } = string.Empty;
    public int? HomeworkId { get; set; }
}