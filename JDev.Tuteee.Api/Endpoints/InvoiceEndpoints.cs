namespace JDev.Tuteee.Api.Endpoints;

using DB;
using ApiClient.DTOs;
using Mapping;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

public class InvoiceEndpoints : IEndpoints
{
    public void MapRoutes(IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapPost("/invoices",
            async Task<Results<Created, NotFound>>(InvoiceDto dto, Context context, CancellationToken token) =>
            {
                var invoice = InvoiceMap.Map(dto);
                foreach (var lessonDto in dto.Lessons)
                {
                    var lesson = await context.Lessons.FindAsync([lessonDto.LessonId], cancellationToken: token);
                    if (lesson is null) return TypedResults.NotFound();
                    invoice.Lessons.Add(lesson);
                }

                await context.Invoices.AddAsync(invoice, cancellationToken: token);
                await context.SaveChangesAsync(token);
                return TypedResults.Created();
            });

        routeBuilder.MapGet("/invoices",
            async (Context context, CancellationToken token) =>
            {
                var entities = await context.Invoices
                    .Include(i => i.Client)
                    .Include(i => i.Lessons)
                    .ToListAsync(cancellationToken: token);
                return TypedResults.Ok(entities.Select(InvoiceMap.Map));
            });

        routeBuilder.MapGet("/invoices/{id:int}",
            async Task<Results<Ok<InvoiceDto>, NotFound>> (int id, Context context, CancellationToken token) =>
            {
                var invoice = await context.Invoices
                    .Include(i => i.Lessons)
                    .SingleOrDefaultAsync(i => i.InvoiceId == id, cancellationToken: token);
                return invoice is null ? TypedResults.NotFound() : TypedResults.Ok(InvoiceMap.Map(invoice));
            });
    }
}