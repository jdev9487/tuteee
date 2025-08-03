namespace JDev.Tuteee.Api.Endpoints;

using Entities;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

public class LoginEndpoints : IEndpoints
{
    public void MapRoutes(IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapPost("/login",
            async Task<Results<Ok<string>, UnauthorizedHttpResult>> ([FromBody] UserLogin userLogin, [FromServices] UserManager<User> manager,
                CancellationToken _) =>
            {
                var user = await manager.FindByEmailAsync(userLogin.Username);
                if (user is null) return TypedResults.Unauthorized();
                if (await manager.CheckPasswordAsync(user, userLogin.Password))
                {
                    var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                    };
        
                    var key = new SymmetricSecurityKey("your_super_secret_key_that_cannot_be_discovered_ever"u8.ToArray());
                    var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        
                    var token = new JwtSecurityToken(
                        issuer: "yourdomain.com",
                        audience: "yourdomain.com",
                        claims: claims,
                        expires: DateTime.Now.AddMinutes(30),
                        signingCredentials: credentials);
        
                    return TypedResults.Ok(new JwtSecurityTokenHandler().WriteToken(token));
                }
        
                return TypedResults.Unauthorized();
            });
    }
}