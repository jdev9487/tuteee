namespace JDev.Tuteee.Rest.ApiClient.DTOs;

public class LessonAttachmentDto
{
    public int LessonAttachmentId { get; init; }
    public string FileName { get; set; } = string.Empty;
    public string TemporaryFileName { get; set; } = string.Empty;
    public int? LessonId { get; set; }
}