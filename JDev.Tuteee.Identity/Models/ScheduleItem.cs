namespace JDev.Tuteee.Identity.Models;

public class ScheduleItem : IEquatable<ScheduleItem>
{
    public string Name { get; set; } = default!;
    public DateTime Start { get; init; }
    public DateTime End { get; init; }
    public string? Link { get; init; }

    public bool Equals(ScheduleItem? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Name == other.Name && Start.Equals(other.Start) && End.Equals(other.End);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((ScheduleItem)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Name, Start, End);
    }
}