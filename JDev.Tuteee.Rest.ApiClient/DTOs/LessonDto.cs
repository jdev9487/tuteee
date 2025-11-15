namespace JDev.Tuteee.Rest.ApiClient.DTOs;

public class LessonDto
{
    public int? LessonId { get; set; }
    public DateOnly Date { get; set; }
    public TimeOnly Start { get; set; }
    public TimeSpan Duration { get; set; }
    public bool EmailSent { get; set; }
    public int TuteeId { get; set; }
    public TuteeDto? Tutee { get; set; } = default!;
    public int? InvoiceId { get; set; }
    public InvoiceDto? Invoice { get; set; }
    public string? HomeworkInstructions { get; set; }
    
    public TimeOnly End => Start.Add(Duration);
    public DateTime DateTimeStart => Date.ToDateTime(Start);
    public DateTime DateTimeEnd => DateTimeStart.Add(Duration);
    public string TuteeName => $"{Tutee?.FirstName} {Tutee?.LastName}";
    public decimal Cost => (decimal)CostAsDouble / 100;
    public string CostString => Cost.ToString("0.00");
    
    private double CostAsDouble
    {
        get
        {
            if (Tutee is null) return 0;
            var rates = Tutee.Rates;
            var activeRate = rates
                .Where(r => r.DateEnabled <= Date)
                .MaxBy(r => r.DateEnabled);
            return 
                activeRate.PencePerHour * Duration.TotalHours;
        }
    }
}