namespace JDev.Tuteee.Api.Endpoints;

using ApiClient;
using DB;
using Mapping;
using ApiClient.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;

public class ClientEndpoints : IEndpoints
{
    public void MapRoutes(IEndpointRouteBuilder routeBuilder)
    {
        var groupBuilder = routeBuilder.MapGroup($"/{Endpoint.ClientBase}");
        groupBuilder.MapGet("/{id:int}",
            async Task<Results<Ok<ClientDto>, NotFound>> (int id, Context context, CancellationToken token) =>
            {
                var account = await context.Clients
                    .Include(t => t.Tutees)
                    .SingleOrDefaultAsync(g => g.ClientId == id, cancellationToken: token);
                return account is null ? TypedResults.NotFound() : TypedResults.Ok(ClientMap.Map(account));
            });

        groupBuilder.MapGet("",
            async (Context context, CancellationToken token) =>
            {
                var entities = await context.Clients
                    .Include(g => g.Tutees)
                    .ToListAsync(cancellationToken: token);
                return TypedResults.Ok(entities.Select(ClientMap.Map));
            });

        groupBuilder.MapPost("",
            async (ClientDto dto, Context context, CancellationToken token) =>
            {
                var entity = ClientMap.Map(dto);
                await context.Clients.AddAsync(entity, token);
                await context.SaveChangesAsync(token);
                return TypedResults.Created();
            });
    }
}