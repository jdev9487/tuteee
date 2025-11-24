namespace JDev.Tuteee.Rest.ApiClient.DTOs;

using CustomTypes;

public class TuteeDto
{
    public int? TuteeId { get; init; }
    public int StakeholderId { get; init; }
    public string FirstName { get; init; } = default!;
    public string LastName { get; init; } = default!;
    public string EmailAddress { get; init; } = default!;
    public PhoneNumber PhoneNumber { get; init; } = default!;
    public required int ClientId { get; init; }
    public ClientDto? Client { get; init; }
    public IEnumerable<LessonDto>? Lessons { get; init; } = [];
    public required IEnumerable<RateDto> Rates { get; init; } = [];
    public IEnumerable<ReservationSlotDto> ReservationSlots { get; init; } = [];
}