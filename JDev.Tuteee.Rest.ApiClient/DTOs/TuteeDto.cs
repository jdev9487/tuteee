namespace JDev.Tuteee.Rest.ApiClient.DTOs;

public class TuteeDto
{
    public int? TuteeId { get; set; }
    public required string FirstName { get; set; } = default!;
    public required string LastName { get; set; } = default!;
    public required string EmailAddress { get; set; } = default!;
    public required int ClientId { get; set; }
    public ClientDto Client { get; set; } = default!;
    public IEnumerable<LessonDto>? Lessons { get; set; } = [];
    public required IEnumerable<RateDto> Rates { get; set; } = [];
    public string ActiveRate => ToPoundsPerHour(Rates.MaxBy(r => r.ActiveFrom).PencePerHour);
    private static string ToPoundsPerHour(int pencePerHour) => (pencePerHour / 100).ToString("0.00");
}