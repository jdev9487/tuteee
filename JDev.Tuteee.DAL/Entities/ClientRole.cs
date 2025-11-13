namespace JDev.Tuteee.DAL.Entities;

using Core.EfCore;

public class ClientRole : BaseEntity
{
    public int ClientRoleId { get; set; }
    public int StakeholderId { get; set; }
    public virtual Stakeholder Stakeholder { get; set; } = default!;
    public virtual IList<TuteeRole> TuteeRoles { get; set; } = [];
    public virtual IList<Invoice> Invoices { get; set; } = [];
}