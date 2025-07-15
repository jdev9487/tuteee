namespace JDev.Tuteee.Api.Entities;

public class Guardian
{
    public int GuardianId { get; set; }
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string EmailAddress { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public IList<Tutee> Tutees { get; set; } = [];
}