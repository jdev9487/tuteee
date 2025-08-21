namespace JDev.Tuteee.Api.Entities;

public class Invoice
{
    public int InvoiceId { get; set; }
    public bool Paid { get; set; }
    public int ClientId { get; set; }
    public Client Client { get; set; } = default!;
    public IList<Lesson> Lessons { get; set; } = [];
}