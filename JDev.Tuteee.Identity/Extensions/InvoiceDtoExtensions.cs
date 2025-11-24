namespace JDev.Tuteee.Identity.Extensions;

using Rest.ApiClient.DTOs;

public static class InvoiceDtoExtensions
{
    public static decimal GetAmount(this InvoiceDto dto) => dto.Lessons.Sum(l => l.Cost);
    public static string GetMonth(this InvoiceDto dto) => dto.From.ToString("Y");
}