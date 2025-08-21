namespace JDev.Tuteee.Api.Mapping;

using ApiClient.DTOs;
using Entities;

public static class InvoiceMap
{
    public static Invoice Map(InvoiceDto dto) =>
        new()
        {
            Paid = dto.Paid,
            ClientId = dto.ClientId,
        };

    public static InvoiceDto Map(Invoice entity) =>
        new()
        {
            InvoiceId = entity.InvoiceId,
            ClientId = entity.ClientId,
            Client = ClientMap.Map(entity.Client),
            Paid = entity.Paid,
            Lessons = entity.Lessons.Select(LessonMap.Map)
        };
}