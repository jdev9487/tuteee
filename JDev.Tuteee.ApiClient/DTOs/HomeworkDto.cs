namespace JDev.Tuteee.ApiClient.DTOs;

public class HomeworkDto
{
    public int? HomeworkId { get; set; }
    public string Instructions { get; set; } = string.Empty;
    public int LessonId { get; set; }
    public IEnumerable<HomeworkAttachmentDto>? HomeworkAttachments { get; set; } = [];
}