namespace JDev.Tuteee.Identity;

public static class Routing
{
    public const string Login = $"{AccountBase}/login";
    public const string Logout = $"{AccountBase}/logout";
    public const string Clients = $"{ClientBase}";
    public const string Client = Clients + "/{ClientId:int}";
    public const string Invoices = "invoices";
    public const string Lesson = "lessons/{LessonId:int}";
    public const string Tutee = "tutees/{TuteeId:int}";

    private const string ClientBase = "clients";
    private const string AccountBase = "account";
}