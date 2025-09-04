namespace JDev.Tuteee.Rest.Api.Endpoints;

using DAL;
using ApiClient;
using AutoMapper;
using ApiClient.DTOs;
using Microsoft.EntityFrameworkCore;

public class InvoiceEndpoints(IMapper mapper) : IEndpoints
{
    public void MapRoutes(IEndpointRouteBuilder routeBuilder)
    {
        var groupBuilder = routeBuilder.MapGroup($"/{Endpoint.InvoiceBase}");
        groupBuilder.MapGet("",
            async (Context context, CancellationToken token) =>
            {
                var entities = await context.Invoices.ToListAsync(token);
                return TypedResults.Ok(entities.Select(mapper.Map<InvoiceDto>));
            });
    }
}