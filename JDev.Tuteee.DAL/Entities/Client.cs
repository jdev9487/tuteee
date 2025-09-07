namespace JDev.Tuteee.DAL.Entities;

using Core.EfCore;

public class Client : BaseEntity
{
    public int ClientId { get; set; }
    public string HolderFirstName { get; set; } = default!;
    public string HolderLastName { get; set; } = default!;
    public string EmailAddress { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public virtual IList<Tutee> Tutees { get; set; } = [];
    public virtual IList<Invoice> Invoices { get; set; } = [];
}