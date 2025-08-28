namespace JDev.Tuteee.ApiClient.DTOs;

public class LessonDto
{
    public int? LessonId { get; set; }
    public DateTimeOffset StartTime { get; set; }
    public DateTimeOffset EndTime { get; set; }
    public int TuteeId { get; set; }
    public TuteeDto Tutee { get; set; } = default!;
    public int? InvoiceId { get; set; }
    public InvoiceDto? Invoice { get; set; }
    public string? HomeworkInstructions { get; set; }
    public decimal Rate => (decimal)RateAsDouble / 100;
    
    private double RateAsDouble
    {
        get
        {
            var rates = Tutee.Rates;
            var activeRate = rates
                .Where(r => r.ActiveFrom < StartTime)
                .MaxBy(r => r.ActiveFrom);
            return 
                activeRate.PencePerHour * (EndTime - StartTime).TotalHours;
        }
    }
}