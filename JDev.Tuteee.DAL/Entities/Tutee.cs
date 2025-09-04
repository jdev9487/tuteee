namespace JDev.Tuteee.DAL.Entities;

public class Tutee : BaseEntity
{
    public int TuteeId { get; set; }
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string EmailAddress { get; set; } = default!;
    public int ClientId { get; set; }
    public virtual Client Client { get; set; } = default!;
    public virtual IList<Lesson> Lessons { get; set; } = [];
    public virtual required IList<Rate> Rates { get; set; } = [];
}