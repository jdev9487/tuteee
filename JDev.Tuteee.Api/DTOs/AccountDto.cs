namespace JDev.Tuteee.Api.DTOs;

public class AccountDto
{
    public int? AccountId { get; set; }
    public required string HolderFirstName { get; set; } = default!;
    public required string HolderLastName { get; set; } = default!;
    public required string EmailAddress { get; set; } = default!;
    public required string PhoneNumber { get; set; } = default!;
    public IEnumerable<TuteeDto>? Tutees { get; set; } = [];
}