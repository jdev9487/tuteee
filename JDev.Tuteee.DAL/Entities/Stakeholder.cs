namespace JDev.Tuteee.DAL.Entities;

using Core.EfCore;
using CustomTypes;

public class Stakeholder : BaseEntity
{
    public int StakeholderId { get; set; }
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string EmailAddress { get; set; } = default!;
    public PhoneNumber PhoneNumber { get; set; } = default!;
    public virtual TuteeRole? TuteeRole { get; set; }
    public virtual ClientRole? ClientRole { get; set; }
}