namespace JDev.Tuteee.Api;

public class Auth
{
    public string SymmetricSecurityKey { get; init; } = default!;
    public string AdminUsername { get; init; } = default!;
    public string AdminPassword { get; init; } = default!;
}