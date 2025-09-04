namespace JDev.Tuteee.Rest.Api.Endpoints;

using ApiClient;
using AutoMapper;
using ApiClient.DTOs;
using DAL.Entities;
using DAL.Repository;

public class InvoiceEndpoints(IMapper mapper) : IEndpoints
{
    public void MapRoutes(IEndpointRouteBuilder routeBuilder)
    {
        var groupBuilder = routeBuilder.MapGroup($"/{Endpoint.InvoiceBase}");
        groupBuilder.MapGet("",
            async (IGenericRepository repo, CancellationToken token) =>
            {
                var entities = await repo.GetListAsync<Invoice>(token);
                return TypedResults.Ok(entities.Select(mapper.Map<InvoiceDto>));
            });
    }
}