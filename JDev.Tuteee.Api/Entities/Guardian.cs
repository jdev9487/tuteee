namespace JDev.Tuteee.Api.Entities;

public class Guardian
{
    public virtual int GuardianId { get; set; }
    public virtual string FirstName { get; set; } = default!;
    public virtual string LastName { get; set; } = default!;
    public virtual string EmailAddress { get; set; } = default!;
    public virtual string PhoneNumber { get; set; } = default!;
    public virtual IList<Tutee> Tutees { get; set; } = [];
}