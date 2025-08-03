namespace JDev.Tuteee.Api.DB;

using Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class IdentityContext(IConfiguration configuration) : IdentityDbContext<User>
{
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlite(configuration.GetConnectionString("IdentityDatabase"));
    }
}