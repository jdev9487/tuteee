namespace JDev.Tuteee.Rest.ApiClient.DTOs;

using Enums;

public class ReservationSlotDto
{
    public DateOnly ReferenceDate { get; set; }
    public TimeOnly Time { get; set; }
    public TimeSpan Duration { get; set; }
    public ReservationSlotType Type { get; set; }
    private int Period => Type switch
    {
        ReservationSlotType.Weekly => 7,
        ReservationSlotType.Biweekly => 14,
        _ => throw new ArgumentOutOfRangeException()
    };
    public int TuteeId { get; set; }
    public TuteeDto? Tutee { get; set; }

    public IEnumerable<ReservationSlotLimit> GetReservedSlots(DateTime start, DateTime end)
    {
        if (start > end) return [];
        var startDayNumber = DateOnly.FromDateTime(start).DayNumber;
        var endDayNumber = DateOnly.FromDateTime(end).DayNumber;
        return Enumerable.Range(startDayNumber, endDayNumber - startDayNumber + 1)
            .Where(i => (i - ReferenceDate.DayNumber) % Period == 0)
            .Select(i => new ReservationSlotLimit
            {
                Start = DateOnly.FromDayNumber(i).ToDateTime(Time),
                End = DateOnly.FromDayNumber(i).ToDateTime(Time).Add(Duration)
            });
    }
    
    public ReservationSlotLimit NextReservedSlot()
    {
        var daysToAdd = (Period - DateOnly.FromDateTime(DateTime.Today).DayNumber + ReferenceDate.DayNumber) % Period;
        if (daysToAdd < 0) daysToAdd += Period;
        return new ReservationSlotLimit
        {
            Start = DateOnly.FromDateTime(DateTime.Today.AddDays(daysToAdd)).ToDateTime(Time),
            End = DateOnly.FromDateTime(DateTime.Today.AddDays(daysToAdd)).ToDateTime(Time).Add(Duration)
        };
    }
}

public class ReservationSlotLimit
{
    public DateTime Start { get; init; }
    public DateTime End { get; init; }

    public override string ToString() => $"{Start:f} - {End:t}";
}