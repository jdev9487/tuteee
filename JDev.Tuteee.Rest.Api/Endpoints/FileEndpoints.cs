namespace JDev.Tuteee.Rest.Api.Endpoints;

using ApiClient.DTOs;
using Microsoft.Extensions.Options;

public class FileEndpoints(IOptions<AppSettings> options) : IEndpoints
{
    private readonly AppSettings _appSettings = options.Value;
    
    public void MapRoutes(IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapPost("/temporary-files",
            async (FileDto dto, CancellationToken token) =>
            {
                await File.WriteAllBytesAsync(Path.Join(_appSettings.TempDirectory, dto.FileName), dto.Contents, token);
                return TypedResults.Created();
            });
    }
}