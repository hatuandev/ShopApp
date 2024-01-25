using OpenIddictAPI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddOpenIddictServices(builder.Configuration);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    await SeedClientCredentials(app.Services, builder.Configuration);
}

app.UseCors("AllowAllOrigins");

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

static async Task SeedClientCredentials(IServiceProvider serviceProvider, IConfiguration configuration)
{
    using var scope = serviceProvider.CreateScope();

    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    await context.Database.EnsureCreatedAsync();

    var manager = scope.ServiceProvider.GetRequiredService<IOpenIddictApplicationManager>();

    // API application CC
    string clientId = configuration["OpenIddict:ClientId"];
    string clientSecret = configuration["OpenIddict:ClientSecret"];
    if (await manager.FindByClientIdAsync(clientId) == null)
    {
        await manager.CreateAsync(new OpenIddictApplicationDescriptor
        {
            ClientId = clientId,
            ClientSecret = clientSecret,
            Permissions =
            {
                OpenIddictConstants.Permissions.Endpoints.Authorization,
                OpenIddictConstants.Permissions.Endpoints.Token,
                OpenIddictConstants.Permissions.Endpoints.Logout,

                OpenIddictConstants.Permissions.GrantTypes.Password,
                OpenIddictConstants.Permissions.GrantTypes.RefreshToken,

                OpenIddictConstants.Permissions.Scopes.Email,
                OpenIddictConstants.Permissions.Scopes.Profile,
                OpenIddictConstants.Permissions.Scopes.Roles,
            }
        });
    }

    var _userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
    var _roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    // Default roles
    var administratorRole = new IdentityRole("Administrator");
    if (_roleManager.Roles.All(r => r.Name != administratorRole.Name))
    {
        await _roleManager.CreateAsync(administratorRole);
    }

    // Default users
    var administrator = new ApplicationUser { UserName = "admin", Email = "administrator@localhost" };
    if (_userManager.Users.All(u => u.UserName != administrator.UserName))
    {
        await _userManager.CreateAsync(administrator, "Admin@123");
        if (!string.IsNullOrWhiteSpace(administratorRole.Name))
        {
            await _userManager.AddToRolesAsync(administrator, new[] { administratorRole.Name });
        }
    }

    var userRole = new IdentityRole("User");
    if (_roleManager.Roles.All(r => r.Name != userRole.Name))
    {
        await _roleManager.CreateAsync(userRole);
    }
}