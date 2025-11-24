namespace JDev.Tuteee.Rest.ApiClient.DTOs;

public class LessonAttachmentDto
{
    public int LessonAttachmentId { get; init; }
    public string FileName { get; init; } = string.Empty;
    public string TemporaryFileName { get; init; } = string.Empty;
    public int? LessonId { get; set; }
}