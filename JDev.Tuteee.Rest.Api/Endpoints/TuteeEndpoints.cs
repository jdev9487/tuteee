namespace JDev.Tuteee.Rest.Api.Endpoints;

using ApiClient;
using AutoMapper;
using ApiClient.DTOs;
using DAL;
using DAL.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

public class TuteeEndpoints(IMapper mapper) : IEndpoints
{
    public void MapRoutes(IEndpointRouteBuilder routeBuilder)
    {
        var groupBuilder = routeBuilder.MapGroup($"/{Endpoint.TuteeBase}");
        MapRates(groupBuilder.MapGroup("/{tuteeId:int}/rates"));
        groupBuilder.MapGet("/{id:int}",
            async Task<Results<Ok<TuteeDto>, NotFound>> (int id, Context context, CancellationToken token) =>
            {
                var entity = await context.Tutees.FindAsync([id], token);
                return entity is null ? TypedResults.NotFound() : TypedResults.Ok(mapper.Map<TuteeDto>(entity));
            });
        
        groupBuilder.MapGet("",
            async (Context context, CancellationToken token) =>
            {
                var entities = await context.Tutees.ToListAsync(token);
                return TypedResults.Ok(entities.Select(mapper.Map<TuteeDto>));
            });
        
        groupBuilder.MapPost("",
            async (TuteeDto dto, Context context, CancellationToken token) =>
            {
                var entity = mapper.Map<Tutee>(dto);
                await context.Tutees.AddAsync(entity, token);
                await context.SaveChangesAsync(token);
                return TypedResults.Created();
            });
    }

    private void MapRates(IEndpointRouteBuilder group)
    {
        group.MapPost("",
            async Task<Results<Created, NotFound>>(int tuteeId, RateDto dto, Context context, CancellationToken token) =>
            {
                var tutee = await context.Tutees.FindAsync([tuteeId], token);
                if (tutee is null) return TypedResults.NotFound();
                var rateEntity = mapper.Map<Rate>(dto);
                tutee.Rates.Add(rateEntity);
                await context.SaveChangesAsync(token);
                return TypedResults.Created();
            });
    }
}