namespace JDev.Tuteee.DAL.Entities;

using Core.EfCore;

public class InvoiceSnapshot : BaseEntity
{
    public int InvoiceSnapshotId { get; set; }
    public int InvoiceId { get; set; }
    public virtual Invoice Invoice { get; set; } = default!;
    public string Path { get; set; } = default!;
}