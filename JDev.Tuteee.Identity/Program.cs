using JDev.Tuteee.ApiClient;
using JDev.Tuteee.Identity;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using JDev.Tuteee.Identity.Components;
using JDev.Tuteee.Identity.Components.Account;
using JDev.Tuteee.Identity.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<AdminAuth>(builder.Configuration.GetSection("AdminAuth"));

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = IdentityConstants.ApplicationScheme;
        options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
    })
    .AddIdentityCookies();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
                       throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentityCore<ApplicationUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager()
    .AddRoleManager<RoleManager<IdentityRole>>()
    .AddRoleStore<RoleStore<IdentityRole, ApplicationDbContext>>()
    .AddDefaultTokenProviders();

var apiUrl = builder.Configuration.GetSection("ApiUrl").Value;
if (apiUrl is null) throw new InvalidOperationException("API config missing");
builder.Services.AddApiClient(apiUrl);
builder.Services.AddBlazorBootstrap();

var app = builder.Build();

app.UsePathBase("/tutoring");

await using var scope = app.Services.CreateAsyncScope();
var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
await context.Database.MigrateAsync();

await using var roleScope = app.Services.CreateAsyncScope();
var roleManager = roleScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
if (!await roleManager.RoleExistsAsync("Admin"))
    await roleManager.CreateAsync(new IdentityRole("Admin"));

await using var userScope = app.Services.CreateAsyncScope();
var adminAuth = userScope.ServiceProvider.GetRequiredService<IOptions<AdminAuth>>().Value;
var userManager = userScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
if (await userManager.FindByEmailAsync(adminAuth.Username) is null)
{
    var user = new ApplicationUser { Email = adminAuth.Username, UserName = adminAuth.Username };
    await userManager.CreateAsync(user, adminAuth.Password);
    await userManager.AddToRoleAsync(user, "Admin");
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.UseAuthorization();

app.MapAdditionalIdentityEndpoints();

app.Run();