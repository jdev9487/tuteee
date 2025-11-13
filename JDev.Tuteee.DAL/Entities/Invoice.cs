namespace JDev.Tuteee.DAL.Entities;

using Core.EfCore;

public class Invoice : BaseEntity
{
    public int InvoiceId { get; set; }
    public bool Paid { get; set; }
    public int ClientRoleId { get; set; }
    public virtual ClientRole ClientRole { get; set; } = default!;
    public virtual IList<Lesson> Lessons { get; set; } = [];
}