namespace JDev.Tuteee.DAL.Entities;

using Core.EfCore;

public class Rate : BaseEntity
{
    public int RateId { get; set; }
    public int PencePerHour { get; set; }
    public DateOnly DateEnabled { get; set; }
    public int TuteeRoleId { get; set; }
    public virtual TuteeRole TuteeRole { get; set; } = default!;
}