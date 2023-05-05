namespace TutorTracker.Api.Parsing;

public class DateParser : IDateParser
{
    public PeriodDto GetPeriod(int? month, int? year)
    {
        DateTimeOffset start;
        DateTimeOffset end;
        if (year is null)
        {
            start = DateTimeOffset.MinValue;
            end = DateTimeOffset.MaxValue;
        }
        else
        {
            if (month is null)
            {
                start = new DateTimeOffset(year.Value, 1, 0, 0, 0, 0, TimeSpan.Zero);
                end = new DateTimeOffset(year.Value, 12, 0, 0, 0, 0, TimeSpan.Zero).AddMicroseconds(-1);
            }
            else
            {
                start = new DateTimeOffset(year.Value, month.Value, 0, 0, 0, 0, TimeSpan.Zero);
                end = new DateTimeOffset(year.Value, month.Value, 0, 0, 0, 0, TimeSpan.Zero)
                    .AddMonths(1)
                    .AddMicroseconds(-1);
            }
        }
        return new PeriodDto
        {
            Start = start,
            End = end
        };
    }
}