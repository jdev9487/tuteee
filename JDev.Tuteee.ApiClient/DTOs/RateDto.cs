namespace JDev.Tuteee.ApiClient.DTOs;

public class RateDto
{
    public int RateId { get; set; }
    public int PencePerHour { get; set; }
    public DateTimeOffset ActiveFrom { get; set; }
    public int TuteeId { get; set; }
    public TuteeDto Tutee { get; set; } = default!;
}