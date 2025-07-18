namespace JDev.Tuteee.Api.Entities;

public class Account
{
    public int AccountId { get; set; }
    public string HolderFirstName { get; set; } = default!;
    public string HolderLastName { get; set; } = default!;
    public string EmailAddress { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public IList<Tutee> Tutees { get; set; } = [];
}