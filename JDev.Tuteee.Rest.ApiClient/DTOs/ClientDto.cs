namespace JDev.Tuteee.Rest.ApiClient.DTOs;

using CustomTypes;

public class ClientDto
{
    public int? ClientId { get; set; }
    public required string HolderFirstName { get; set; } = default!;
    public required string HolderLastName { get; set; } = default!;
    public string HolderName => $"{HolderFirstName} {HolderLastName}";
    public required string EmailAddress { get; set; } = default!;
    public required PhoneNumber PhoneNumber { get; set; } = default!;
    public IEnumerable<TuteeDto>? Tutees { get; set; } = [];
    public IList<InvoiceDto> Invoices { get; set; } = [];
}