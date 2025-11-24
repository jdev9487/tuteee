namespace JDev.Tuteee.Identity.Extensions;

using Rest.ApiClient.DTOs;
using Rest.ApiClient.Enums;

public static class ReservationSlotDtoExtensions
{
    private static int GetPeriod(this ReservationSlotDto dto) => dto.Type switch
    {
        ReservationSlotType.Weekly => 7,
        ReservationSlotType.Biweekly => 14,
        _ => throw new ArgumentOutOfRangeException()
    };

    public static IEnumerable<ReservationSlotLimit> GetReservedSlots(this ReservationSlotDto dto, DateTime start, DateTime end)
    {
        if (start > end) return [];
        var startDayNumber = DateOnly.FromDateTime(start).DayNumber;
        var endDayNumber = DateOnly.FromDateTime(end).DayNumber;
        return Enumerable.Range(startDayNumber, endDayNumber - startDayNumber + 1)
            .Where(i => (i - dto.ReferenceDate.DayNumber) % dto.GetPeriod() == 0)
            .Select(i => new ReservationSlotLimit
            {
                Start = DateOnly.FromDayNumber(i).ToDateTime(dto.Time),
                End = DateOnly.FromDayNumber(i).ToDateTime(dto.Time).Add(dto.Duration)
            });
    }
    
    public static string ToDisplay(this ReservationSlotDto dto) => dto.Type switch
    {
        ReservationSlotType.Weekly => $"{dto.ReferenceDate.DayOfWeek}s, {dto.Time:h:mm tt} - {dto.Time.Add(dto.Duration):h:mm tt}",
        ReservationSlotType.Biweekly => $"Every other {dto.ReferenceDate.DayOfWeek}, {dto.Time:h:mm tt} - {dto.Time.Add(dto.Duration):h:mm tt} (next: {dto.GetNextReservedSlot().Start:M})"
    };
    
    private static ReservationSlotLimit GetNextReservedSlot(this ReservationSlotDto dto)
    {
        var daysToAdd = (dto.GetPeriod() - DateOnly.FromDateTime(DateTime.Today).DayNumber + dto.ReferenceDate.DayNumber) % dto.GetPeriod();
        if (daysToAdd < 0) daysToAdd += dto.GetPeriod();
        return new ReservationSlotLimit
        {
            Start = DateOnly.FromDateTime(DateTime.Today.AddDays(daysToAdd)).ToDateTime(dto.Time),
            End = DateOnly.FromDateTime(DateTime.Today.AddDays(daysToAdd)).ToDateTime(dto.Time).Add(dto.Duration)
        };
    }
}