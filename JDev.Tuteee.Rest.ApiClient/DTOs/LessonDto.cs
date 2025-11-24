namespace JDev.Tuteee.Rest.ApiClient.DTOs;

public class LessonDto
{
    public int? LessonId { get; set; }
    public DateOnly Date { get; set; }
    public TimeOnly Start { get; set; }
    public TimeSpan Duration { get; set; }
    public bool EmailSent { get; set; }
    public bool Paid { get; set; }
    public int TuteeId { get; set; }
    public TuteeDto? Tutee { get; set; } = default!;
    public int? InvoiceId { get; set; }
    public InvoiceDto? Invoice { get; set; }
    public string? HomeworkInstructions { get; set; }
    public string? Title { get; set; }
    public string? Summary { get; set; }
}