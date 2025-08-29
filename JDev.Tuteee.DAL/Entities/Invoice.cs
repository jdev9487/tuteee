namespace JDev.Tuteee.DAL.Entities;

public class Invoice
{
    public int InvoiceId { get; set; }
    public bool Paid { get; set; }
    public int ClientId { get; set; }
    public virtual Client Client { get; set; } = default!;
    public virtual IList<Lesson> Lessons { get; set; } = [];
}