namespace JDev.Tuteee.Rest.Api.Endpoints;

using DAL;
using ApiClient;
using AutoMapper;
using DAL.Entities;
using ApiClient.DTOs;
using DAL.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

public class ClientEndpoints(IMapper mapper) : IEndpoints
{
    public void MapRoutes(IEndpointRouteBuilder routeBuilder)
    {
        var groupBuilder = routeBuilder.MapGroup($"/{Endpoint.ClientBase}");
        groupBuilder.MapGet("/{id:int}",
            async Task<Results<Ok<ClientDto>, NotFound>> (int id, IGenericRepository repo, CancellationToken token) =>
            {
                var client = await repo.FindAsync<Client>(id, token);
                return client is null ? TypedResults.NotFound() : TypedResults.Ok(mapper.Map<ClientDto>(client));
            });
        
        groupBuilder.MapGet("",
            async (IGenericRepository repo, CancellationToken token) =>
            {
                var entities = await repo.GetListAsync<Client>(token);
                return TypedResults.Ok(entities.Select(mapper.Map<ClientDto>));
            });
        
        groupBuilder.MapPost("",
            async (ClientDto dto, IGenericRepository repo, CancellationToken token) =>
            {
                var entity = mapper.Map<Client>(dto);
                await repo.AddAsync(entity, token);
                await repo.SaveChangesAsync(token);
                return TypedResults.Created();
            });
    }
}