using Microsoft.IdentityModel.Tokens;
using OpenIddict.Validation.AspNetCore;
using OrderAPI;
using OrderAPI.ConfigurationOptions;
using OrderAPI.Infrastructure.BaseEndpoint;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

var appSettings = new AppSettings();

var configuration = builder.Configuration;
configuration.Bind(appSettings);

var SecurityKey = new SymmetricSecurityKey(Convert.FromBase64String(appSettings.IdentityServerAuthentication.SecurityKey));

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddWebServices();
// Add services to the container.

builder.Services.AddAuthorization();
builder.Services.AddAuthentication(OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = appSettings.IdentityServerAuthentication.Authority;
        options.Audience = appSettings.IdentityServerAuthentication.ApiName;
        options.RequireHttpsMetadata = appSettings.IdentityServerAuthentication.RequireHttpsMetadata;
    });

builder.Services.AddOpenIddict()
    .AddValidation(options =>
    {
        options.SetIssuer(appSettings.IdentityServerAuthentication.Authority);
        options.AddEncryptionKey(SecurityKey);
        options.UseSystemNetHttp();
        options.UseAspNetCore();
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
});

var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.Map("/", () => Results.Redirect("/swagger"));
}
else
{
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapEndpoints();

app.Run();