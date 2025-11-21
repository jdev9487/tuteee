namespace JDev.Tuteee.Grpc.Api.Messages;

public sealed class EmailHomeworkEvent
{
    public int LessonId { get; init; }
    public string To { get; init; } = "";
    public string Copy { get; init; } = "";
    public string Body { get; init; } = "";
    public string Date { get; init; } = "";
}