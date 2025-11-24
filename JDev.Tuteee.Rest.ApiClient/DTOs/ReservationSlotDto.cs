namespace JDev.Tuteee.Rest.ApiClient.DTOs;

using Enums;

public class ReservationSlotDto
{
    public DateOnly ReferenceDate { get; set; }
    public TimeOnly Time { get; set; }
    public TimeSpan Duration { get; set; }
    public ReservationSlotType Type { get; set; }
    public int TuteeId { get; set; }
    public TuteeDto? Tutee { get; set; }
    //
    // public override string ToString() => Type switch
    // {
    //     ReservationSlotType.Weekly => $"{ReferenceDate.DayOfWeek}s, {Time:h:mm tt} - {Time.Add(Duration):h:mm tt}",
    //     ReservationSlotType.Biweekly => $"Every other {ReferenceDate.DayOfWeek}, {Time:h:mm tt} - {Time.Add(Duration):h:mm tt} (next: {this.GetNextReservedSlot().Start:M})"
    // };
}

public class ReservationSlotLimit
{
    public DateTime Start { get; init; }
    public DateTime End { get; init; }

    public override string ToString() => $"{Start:f} - {End:t}";
}