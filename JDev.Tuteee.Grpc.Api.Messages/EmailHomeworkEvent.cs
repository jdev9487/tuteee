namespace JDev.Tuteee.Grpc.Api.Messages;

public sealed class EmailHomeworkEvent
{
    public string To { get; init; } = "";
    public string Body { get; init; } = "";
    public string Date { get; init; } = "";
}