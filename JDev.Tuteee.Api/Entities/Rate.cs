namespace JDev.Tuteee.Api.Entities;

public class Rate
{
    public int RateId { get; set; }
    public int PencePerHour { get; set; }
    public DateTimeOffset ActiveFrom { get; set; }
    public int TuteeId { get; set; }
    public Tutee Tutee { get; set; } = default!;
}