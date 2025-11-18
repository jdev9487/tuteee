namespace JDev.Tuteee.Identity.Models;

public class ScheduleItem
{
    public DateTime Start { get; init; }
    public DateTime End { get; init; }
    public string Text { get; init; } = default!;
    public string? Link { get; init; }
}