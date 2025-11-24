namespace JDev.Tuteee.Rest.ApiClient.DTOs;

public class RateDto
{
    public int RateId { get; init; }
    public int PencePerHour { get; init; }
    public DateOnly DateEnabled { get; init; }
    public int TuteeId { get; init; }
    public TuteeDto Tutee { get; init; } = default!;
}