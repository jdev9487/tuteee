namespace JDev.Tuteee.Rest.ApiClient.DTOs;

using Enums;

public class ReservationSlotDto
{
    public DateOnly ReferenceDate { get; init; }
    public TimeOnly Time { get; init; }
    public TimeSpan Duration { get; init; }
    public ReservationSlotType Type { get; init; }
    public int TuteeId { get; init; }
    public TuteeDto? Tutee { get; init; }
}