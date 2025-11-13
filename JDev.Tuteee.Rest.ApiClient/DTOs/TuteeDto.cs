namespace JDev.Tuteee.Rest.ApiClient.DTOs;

using CustomTypes;

public class TuteeDto
{
    public int? TuteeId { get; set; }
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string EmailAddress { get; set; } = default!;
    public PhoneNumber PhoneNumber { get; set; } = default!;
    public required int ClientId { get; set; }
    public ClientDto Client { get; set; } = default!;
    public IEnumerable<LessonDto>? Lessons { get; set; } = [];
    public required IEnumerable<RateDto> Rates { get; set; } = [];
    public int ActiveRate => Rates.MaxBy(r => r.ActiveFrom).PencePerHour;
}