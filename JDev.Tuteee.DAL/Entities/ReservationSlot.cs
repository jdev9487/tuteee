namespace JDev.Tuteee.DAL.Entities;

using Core.EfCore;
using Enums;

public class ReservationSlot : BaseEntity
{
    public int ReservationSlotId { get; set; }
    public int TuteeRoleId { get; set; }
    public virtual TuteeRole TuteeRole { get; set; } = default!;
    public ReservationSlotType Type { get; set; }
}