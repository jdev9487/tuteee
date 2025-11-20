namespace JDev.Tuteee.Rest.Api.Endpoints;

using ApiClient;
using AutoMapper;
using DAL.Entities;
using ApiClient.DTOs;
using Core.EfCore.Repository;

public class ReservationSlotEndpoints(
    IMapper mapper) : IEndpoints
{
    public void MapRoutes(IEndpointRouteBuilder routeBuilder)
    {
        var groupBuilder = routeBuilder.MapGroup($"/{Endpoint.ReservationSlotBase}");
        
        groupBuilder.MapGet("",
            async (IGenericRepository repo, CancellationToken token) =>
            {
                var entities = await repo.GetListAsync<ReservationSlot>(token);
                return TypedResults.Ok(entities.Select(mapper.Map<ReservationSlotDto>));
            });
    }
}