namespace JDev.Tuteee.Rest.ApiClient.DTOs;

public class InvoiceDto
{
    public int InvoiceId { get; init; }
    public int ClientId { get; init; }
    public DateOnly From { get; init; }
    public DateOnly To { get; init; }
    public ClientDto Client { get; init; } = default!;
    public IEnumerable<LessonDto> Lessons { get; init; } = [];
}