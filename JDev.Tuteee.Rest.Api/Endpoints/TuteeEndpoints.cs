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
                var entity = await repo.FindAsync<Tutee>(id, token);
                return entity is null ? TypedResults.NotFound() : TypedResults.Ok(mapper.Map<TuteeDto>(entity));
            });
        
        groupBuilder.MapGet("",
            async (IGenericRepository repo, CancellationToken token) =>
            {
                var entities = await repo.GetListAsync<Tutee>(token);
                return TypedResults.Ok(entities.Select(mapper.Map<TuteeDto>));
            });
        
        groupBuilder.MapPost("",
            async (TuteeDto dto, IGenericRepository repo, CancellationToken token) =>
            {
                var entity = mapper.Map<Tutee>(dto);
                await repo.AddAsync(entity, token);
                await repo.SaveChangesAsync(token);
                return TypedResults.Created();
            });
    }

    private void MapRates(IEndpointRouteBuilder group)
    {
        group.MapPost("",
            async Task<Results<Created, NotFound>>(int tuteeId, RateDto dto, IGenericRepository repo, CancellationToken token) =>
            {
                var tutee = await repo.FindAsync<Tutee>(tuteeId, token);
                if (tutee is null) return TypedResults.NotFound();
                var rateEntity = mapper.Map<Rate>(dto);
                tutee.Rates.Add(rateEntity);
                await repo.SaveChangesAsync(token);
                return TypedResults.Created();
            });
    }
}