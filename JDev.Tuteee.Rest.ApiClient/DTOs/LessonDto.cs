namespace JDev.Tuteee.Rest.ApiClient.DTOs;

public class LessonDto
{
    public int? LessonId { get; init; }
    public DateOnly Date { get; init; }
    public TimeOnly Start { get; set; }
    public TimeSpan Duration { get; set; }
    public bool EmailSent { get; init; }
    public bool Paid { get; init; }
    public int TuteeId { get; init; }
    public TuteeDto? Tutee { get; init; } = default!;
    public int? InvoiceId { get; init; }
    public InvoiceDto? Invoice { get; init; }
    public string? HomeworkInstructions { get; set; }
    public string? Title { get; set; }
    public string? Summary { get; set; }
}