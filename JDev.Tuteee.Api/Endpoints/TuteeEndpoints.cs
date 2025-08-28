namespace JDev.Tuteee.Api.Endpoints;

using DB;
using Entities;
using ApiClient;
using AutoMapper;
using ApiClient.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;

public class TuteeEndpoints(IMapper mapper) : IEndpoints
{
    public void MapRoutes(IEndpointRouteBuilder routeBuilder)
    {
        var groupBuilder = routeBuilder.MapGroup($"/{Endpoint.TuteeBase}");
        groupBuilder.MapGet("/{id:int}",
            async Task<Results<Ok<TuteeDto>, NotFound>> (int id, Context context, CancellationToken token) =>
            {
                var entity = await context.Tutees
                    .SingleOrDefaultAsync(t => t.TuteeId == id, cancellationToken: token);
                return entity is null ? TypedResults.NotFound() : TypedResults.Ok(mapper.Map<TuteeDto>(entity));
            });
        
        groupBuilder.MapGet("",
            async (Context context, CancellationToken token) =>
            {
                var entities = await context.Tutees
                    .ToListAsync(cancellationToken: token);
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
}