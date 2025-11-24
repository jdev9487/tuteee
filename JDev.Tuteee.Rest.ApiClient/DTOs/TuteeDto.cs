namespace JDev.Tuteee.Rest.ApiClient.DTOs;

using CustomTypes;

public class TuteeDto
{
    public int? TuteeId { get; set; }
    public int StakeholderId { get; set; }
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string EmailAddress { get; set; } = default!;
    public PhoneNumber PhoneNumber { get; set; } = default!;
    public required int ClientId { get; set; }
    public ClientDto? Client { get; set; }
    public IEnumerable<LessonDto>? Lessons { get; set; } = [];
    public required IEnumerable<RateDto> Rates { get; set; } = [];
    public IEnumerable<ReservationSlotDto> ReservationSlots { get; set; } = [];
    public int ActiveRate => Rates
        .Where(r => r.DateEnabled <= DateOnly.FromDateTime(DateTime.Today))
        .MaxBy(r => r.DateEnabled).PencePerHour;
    // public bool IsSelfPaying => StakeholderId == Client?.StakeholderId;
}