namespace JDev.Tuteee.Rest.ApiClient.DTOs;

public class RateDto
{
    public int RateId { get; set; }
    public int PencePerHour { get; set; }
    public DateOnly DateEnabled { get; set; }
    public int TuteeId { get; set; }
    public TuteeDto Tutee { get; set; } = default!;
}