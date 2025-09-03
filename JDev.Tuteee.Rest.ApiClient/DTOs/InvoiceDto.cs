namespace JDev.Tuteee.Rest.ApiClient.DTOs;

public class InvoiceDto
{
    public int InvoiceId { get; set; }
    public bool Paid { get; set; }
    public int ClientId { get; set; }
    public ClientDto Client { get; set; } = default!;
    public IEnumerable<LessonDto> Lessons { get; set; } = [];
    public decimal Amount => Lessons.Sum(l => l.Cost);
}