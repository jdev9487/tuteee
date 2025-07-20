namespace JDev.Tuteee.Api.Endpoints;

using ApiClient.DTOs;
using DB;
using Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;

public class AccountEndpoints : IEndpoints
{
    public void MapRoutes(IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapGet("/accounts/{id:int}",
            async Task<Results<Ok<ClientDto>, NotFound>> (int id, Context context, CancellationToken token) =>
            {
                var account = await context.Accounts
                    .Include(t => t.Tutees)
                    .SingleOrDefaultAsync(g => g.AccountId == id, cancellationToken: token);
                return account is null ? TypedResults.NotFound() : TypedResults.Ok(AccountMap.Map(account));
            });
        
        routeBuilder.MapGet("/accounts",
            async (Context context, CancellationToken token) =>
            {
                var entities = await context.Accounts
                    .Include(g => g.Tutees)
                    .ToListAsync(cancellationToken: token);
                return TypedResults.Ok(entities.Select(AccountMap.Map));
            });

        routeBuilder.MapPost("/accounts",
            async (ClientDto dto, Context context, CancellationToken token) =>
            {
                var entity = AccountMap.Map(dto);
                await context.Accounts.AddAsync(entity, token);
                await context.SaveChangesAsync(token);
                return TypedResults.Created();
            });
    }
}