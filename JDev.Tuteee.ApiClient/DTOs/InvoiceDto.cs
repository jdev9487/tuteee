namespace JDev.Tuteee.ApiClient.DTOs;

public class InvoiceDto
{
    public int InvoiceId { get; set; }
    public bool Paid { get; set; }
    public int ClientId { get; set; }
    public IEnumerable<LessonDto> Lessons { get; set; } = [];
}