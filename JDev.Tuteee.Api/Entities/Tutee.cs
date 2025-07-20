namespace JDev.Tuteee.Api.Entities;

public class Tutee
{
    public int TuteeId { get; set; }
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string EmailAddress { get; set; } = default!;
    public int AccountId { get; set; }
    public Account Account { get; set; } = default!;
    public IList<Lesson> Lessons { get; set; } = [];
    public IList<Rate> Rates { get; set; } = [];
}