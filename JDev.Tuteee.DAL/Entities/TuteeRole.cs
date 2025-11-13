namespace JDev.Tuteee.DAL.Entities;

using Core.EfCore;

public class TuteeRole : BaseEntity
{
    public int TuteeRoleId { get; set; }
    public int ClientRoleId { get; set; }
    public virtual ClientRole ClientRole { get; set; } = default!;
    public int TuitionStakeholderId { get; set; }
    public virtual TuitionStakeholder TuitionStakeholder { get; set; } = default!;
    public virtual IList<Lesson> Lessons { get; set; } = [];
    public virtual required IList<Rate> Rates { get; set; } = [];
}