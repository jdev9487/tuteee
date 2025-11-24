namespace JDev.Tuteee.Rest.ApiClient.DTOs;

public class InvoiceDto
{
    public int InvoiceId { get; set; }
    public int ClientId { get; set; }
    public DateOnly From { get; set; }
    public DateOnly To { get; set; }
    public ClientDto Client { get; set; } = default!;
    public IEnumerable<LessonDto> Lessons { get; set; } = [];
}