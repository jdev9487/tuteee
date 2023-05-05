namespace TutorTracker.Api.Parsing;

public interface IDateParser
{
    PeriodDto GetPeriod(int? month, int? year);
}

public class PeriodDto
{
    public DateTimeOffset Start { get; init; }
    public DateTimeOffset End { get; init; }
}