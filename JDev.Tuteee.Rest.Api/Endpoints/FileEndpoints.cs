namespace JDev.Tuteee.Rest.Api.Endpoints;

using ApiClient.DTOs;
using Microsoft.Extensions.Options;

public class FileEndpoints : IEndpoints
{
    public void MapRoutes(IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapPost("/temporary-files",
            async (FileDto dto, CancellationToken token) =>
            {
                await File.WriteAllBytesAsync(Path.Join(Path.GetTempPath(), dto.FileName), dto.Contents, token);
                return TypedResults.Created();
            });
    }
}