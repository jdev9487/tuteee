namespace JDev.Tuteee.Rest.Api.Endpoints;

using ApiClient;
using AutoMapper;
using ApiClient.DTOs;
using DAL;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;

public class ClientEndpoints(IMapper mapper) : IEndpoints
{
    public void MapRoutes(IEndpointRouteBuilder routeBuilder)
    {
        var groupBuilder = routeBuilder.MapGroup($"/{Endpoint.ClientBase}");
        groupBuilder.MapGet("/{id:int}",
            async Task<Results<Ok<ClientDto>, NotFound>> (int id, Context context, CancellationToken token) =>
            {
                var client = await context.Clients
                    .SingleOrDefaultAsync(g => g.ClientId == id, cancellationToken: token);
                return client is null ? TypedResults.NotFound() : TypedResults.Ok(mapper.Map<ClientDto>(client));
            });
        
        groupBuilder.MapGet("",
            async (Context context, CancellationToken token) =>
            {
                var entities = await context.Clients
                    .ToListAsync(cancellationToken: token);
                return TypedResults.Ok(entities.Select(mapper.Map<ClientDto>));
            });
        
        groupBuilder.MapPost("",
            async (ClientDto dto, Context context, CancellationToken token) =>
            {
                var entity = mapper.Map<Client>(dto);
                await context.Clients.AddAsync(entity, token);
                await context.SaveChangesAsync(token);
                return TypedResults.Created();
            });
    }
}