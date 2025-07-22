namespace JDev.Tuteee.ApiClient.DTOs;

public class TemporaryFileDto
{
    public byte[] Contents { get; init; } = default!;
    public string Filename { get; init; } = default!;
}