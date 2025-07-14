namespace JDev.Tuteee.Api.Entities;

public class Tutee
{
    public virtual int TuteeId { get; set; }
    public virtual string FirstName { get; set; } = default!;
    public virtual string LastName { get; set; } = default!;
    public virtual string EmailAddress { get; set; } = default!;
    public virtual int GuardianId { get; set; }
    public virtual Guardian Guardian { get; set; } = default!;
    public virtual IList<Lesson> Lessons { get; set; } = [];
}