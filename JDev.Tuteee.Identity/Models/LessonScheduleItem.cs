namespace JDev.Tuteee.Identity.Models;

public class LessonScheduleItem : ScheduleItem
{
    public override string Text => DateTime.Now > Start ? $"{Name} ✅" : Name;
}