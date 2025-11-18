namespace JDev.Tuteee.Rest.ApiClient.DTOs;

using Enums;

public class ReservationSlotDto
{
    public int? ReservationSlotId { get; set; }
    public DateOnly ReferenceDate { get; set; }
    public TimeOnly Time { get; set; }
    public TimeSpan Duration { get; set; }
    public ReservationSlotType Type { get; set; }
    public int TuteeId { get; set; }
    public TuteeDto? Tutee { get; set; } = default!;

    public IEnumerable<ReservationSlotLimit> GetReservedSlots(DateTime start, DateTime end)
    {
        var period = Type switch
        {
            ReservationSlotType.Weekly => 7,
            ReservationSlotType.Biweekly => 14,
            _ => throw new ArgumentOutOfRangeException()
        };
        var startDayNumber = DateOnly.FromDateTime(start).DayNumber;
        var endDayNumber = DateOnly.FromDateTime(end).DayNumber;
        return Enumerable.Range(startDayNumber, endDayNumber - startDayNumber + 1)
            .Where(i => (i - ReferenceDate.DayNumber) % period == 0)
            .Select(i => new ReservationSlotLimit
            {
                Start = DateOnly.FromDayNumber(i).ToDateTime(Time),
                End = DateOnly.FromDayNumber(i).ToDateTime(Time).Add(Duration)
            });
    }
}

public class ReservationSlotLimit
{
    public DateTime Start { get; init; }
    public DateTime End { get; init; }
}