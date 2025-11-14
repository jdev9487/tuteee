namespace JDev.Tuteee.Rest.Api.Endpoints;

using ApiClient;
using AutoMapper;
using DAL.Entities;
using ApiClient.DTOs;
using Core.EfCore.Repository;
using Microsoft.AspNetCore.Http.HttpResults;

public class TuteeEndpoints(IMapper mapper) : IEndpoints
{
    public void MapRoutes(IEndpointRouteBuilder routeBuilder)
    {
        var groupBuilder = routeBuilder.MapGroup($"/{Endpoint.TuteeBase}");
        MapRates(groupBuilder.MapGroup("/{tuteeId:int}/rates"));
        groupBuilder.MapGet("/{id:int}",
            async Task<Results<Ok<TuteeDto>, NotFound>> (int id, IGenericRepository repo, CancellationToken token) =>
            {
                var entity = await repo.FindAsync<TuteeRole>(id, token);
                return entity is null ? TypedResults.NotFound() : TypedResults.Ok(mapper.Map<TuteeDto>(entity));
            });
        
        groupBuilder.MapGet("",
            async (IGenericRepository repo, CancellationToken token) =>
            {
                var entities = await repo.GetListAsync<TuteeRole>(token);
                return TypedResults.Ok(entities.Select(mapper.Map<TuteeDto>));
            });
        
        groupBuilder.MapPost("", // create new stakeholder and assign it with new tutee role
            async Task<Results<Created, NotFound>>(TuteeDto dto, IGenericRepository repo, CancellationToken token) =>
            {
                var clientRole = await repo.FindAsync<ClientRole>(dto.ClientId, token);
                if (clientRole is null) return TypedResults.NotFound();
                var stakeholder = mapper.Map<Stakeholder>(dto);
                var tuteeRole = mapper.Map<TuteeRole>(dto);
                tuteeRole.Stakeholder = stakeholder;
                tuteeRole.ClientRole = clientRole;
                await repo.AddAsync(tuteeRole, token);
                await repo.AddAsync(stakeholder, token);
                await repo.SaveChangesAsync(token);
                return TypedResults.Created();
            });
        
        groupBuilder.MapPost("/role", // assign new role to existing stakeholder
            async Task<Results<Created, NotFound>>(TuteeDto dto, IGenericRepository repo, CancellationToken token) =>
            {
                var clientRole = await repo.FindAsync<ClientRole>(dto.ClientId, token); // existing stakeholder
                if (clientRole is null) return TypedResults.NotFound();
                var tuteeRole = mapper.Map<TuteeRole>(dto);
                tuteeRole.ClientRole = clientRole;
                tuteeRole.Stakeholder = clientRole.Stakeholder;
                await repo.AddAsync(tuteeRole, token);
                await repo.SaveChangesAsync(token);
                return TypedResults.Created();
            });
    }

    private void MapRates(IEndpointRouteBuilder group)
    {
        group.MapPost("",
            async Task<Results<Created, NotFound>>(int tuteeId, RateDto dto, IGenericRepository repo, CancellationToken token) =>
            {
                var tutee = await repo.FindAsync<TuteeRole>(tuteeId, token);
                if (tutee is null) return TypedResults.NotFound();
                var rateEntity = mapper.Map<Rate>(dto);
                tutee.Rates.Add(rateEntity);
                await repo.SaveChangesAsync(token);
                return TypedResults.Created();
            });
    }
}