namespace JDev.Tuteee.Rest.ApiClient.DTOs;

using CustomTypes;

public class ClientDto
{
    public int? ClientId { get; set; }
    public int StakeholderId { get; set; }
    public required string FirstName { get; set; } = default!;
    public required string LastName { get; set; } = default!;
    public string Name => $"{FirstName} {LastName}";
    public required string EmailAddress { get; set; } = default!;
    public required PhoneNumber PhoneNumber { get; set; } = default!;
    public IEnumerable<TuteeDto>? Tutees { get; set; } = [];
    public IList<InvoiceDto> Invoices { get; set; } = [];
}