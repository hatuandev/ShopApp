namespace OpenIddictAPI;

public static class DependencyInjection
{
    public static IServiceCollection AddOpenIddictServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration["ConnectionStringIdentityServer"];
        var SecurityKey = new SymmetricSecurityKey(Convert.FromBase64String(configuration["OpenIddict:SecurityKey"]));

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
            options.UseOpenIddict();
        });

        services.Configure<IdentityOptions>(options =>
        {
            options.ClaimsIdentity.UserNameClaimType = OpenIddictConstants.Claims.Name;
            options.ClaimsIdentity.UserIdClaimType = OpenIddictConstants.Claims.Subject;
            options.ClaimsIdentity.RoleClaimType = OpenIddictConstants.Claims.Role;
        });

        services.AddHttpContextAccessor();

        services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

        services.AddQuartz(options =>
        {
            options.UseMicrosoftDependencyInjectionJobFactory();
            options.UseSimpleTypeLoader();
            options.UseInMemoryStore();
        });

        services.AddQuartzHostedService(options => options.WaitForJobsToComplete = true);

        services.AddCors(options =>
        {
            options.AddPolicy("AllowAllOrigins",
                builder =>
                {
                    builder.AllowCredentials()
                           .WithOrigins(configuration["GatewayApi:Host"])
                           .SetIsOriginAllowedToAllowWildcardSubdomains()
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                });
        });

        services.AddOpenIddict()
                .AddCore(options =>
                {
                    options.UseEntityFrameworkCore()
                           .UseDbContext<ApplicationDbContext>();

                    options.UseQuartz()
                           .SetMinimumAuthorizationLifespan(TimeSpan.FromDays(7))
                           .SetMinimumTokenLifespan(TimeSpan.FromHours(12))
                           .SetMaximumRefireCount(3);
                })
                .AddServer(options =>
                {
                    options.SetTokenEndpointUris("connect/token")
                           .SetAuthorizationEndpointUris("connect/authorize")
                           .SetLogoutEndpointUris("connect/logout")
                           .SetUserinfoEndpointUris("connect/userinfo");

                    options.RegisterScopes(OpenIddictConstants.Scopes.Email, 
                                           OpenIddictConstants.Scopes.Profile, 
                                           OpenIddictConstants.Scopes.Roles, 
                                           OpenIddictConstants.Scopes.OfflineAccess, 
                                           "GatewayApi");

                    options.AllowAuthorizationCodeFlow()
                           .AllowPasswordFlow()
                           .AllowRefreshTokenFlow();

                    options.AddEncryptionKey(SecurityKey);

                    options.AddEphemeralEncryptionKey()
                           .AddEphemeralSigningKey();

                    options.SetAuthorizationCodeLifetime(TimeSpan.FromMinutes(30));
                    options.SetAccessTokenLifetime(TimeSpan.FromMinutes(30));
                    options.SetIdentityTokenLifetime(TimeSpan.FromMinutes(30));
                    options.SetRefreshTokenLifetime(TimeSpan.FromDays(14));

                    options.UseAspNetCore()
                           .EnableAuthorizationEndpointPassthrough()
                           .EnableTokenEndpointPassthrough()
                           .EnableLogoutEndpointPassthrough()
                           .EnableUserinfoEndpointPassthrough()
                           .DisableTransportSecurityRequirement();
                })
                .AddValidation(options =>
                {
                    options.SetIssuer(configuration["GatewayApi:Host"]);
                    options.UseSystemNetHttp();
                });

        return services;
    }
}

