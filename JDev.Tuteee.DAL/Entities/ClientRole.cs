namespace JDev.Tuteee.DAL.Entities;

using Core.EfCore;

public class ClientRole : BaseEntity
{
    public int ClientRoleId { get; set; }
    public int TuitionStakeholderId { get; set; }
    public virtual TuitionStakeholder TuitionStakeholder { get; set; } = default!;
    public virtual IList<TuteeRole> TuteeRoles { get; set; } = [];
    public virtual IList<Invoice> Invoices { get; set; } = [];
}