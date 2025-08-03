namespace JDev.Tuteee.Api.Endpoints;

using System.Security.Claims;
using ApiClient.DTOs;
using DB;
using Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;

public class ClientEndpoints : IEndpoints
{
    public void MapRoutes(IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapGet("/clients/{id:int}",
            async Task<Results<Ok<ClientDto>, NotFound>> (int id, Context context, CancellationToken token) =>
            {
                var account = await context.Clients
                    .Include(t => t.Tutees)
                    .SingleOrDefaultAsync(g => g.ClientId == id, cancellationToken: token);
                return account is null ? TypedResults.NotFound() : TypedResults.Ok(ClientMap.Map(account));
            });

        routeBuilder.MapGet("/clients",
            async (Context context, CancellationToken token) =>
            {
                var entities = await context.Clients
                    .Include(g => g.Tutees)
                    .ToListAsync(cancellationToken: token);
                return TypedResults.Ok(entities.Select(ClientMap.Map));
            }).RequireAuthorization();

        routeBuilder.MapPost("/clients",
            async (ClientDto dto, Context context, CancellationToken token) =>
            {
                var entity = ClientMap.Map(dto);
                await context.Clients.AddAsync(entity, token);
                await context.SaveChangesAsync(token);
                return TypedResults.Created();
            });
    }
}