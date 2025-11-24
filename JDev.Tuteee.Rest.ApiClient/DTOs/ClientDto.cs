namespace JDev.Tuteee.Rest.ApiClient.DTOs;

using CustomTypes;

public class ClientDto
{
    public int? ClientId { get; init; }
    public int StakeholderId { get; init; }
    public required string FirstName { get; init; } = default!;
    public required string LastName { get; init; } = default!;
    public required string EmailAddress { get; init; } = default!;
    public required PhoneNumber PhoneNumber { get; init; } = default!;
    public IEnumerable<TuteeDto>? Tutees { get; init; } = [];
    public IList<InvoiceDto> Invoices { get; init; } = [];
}