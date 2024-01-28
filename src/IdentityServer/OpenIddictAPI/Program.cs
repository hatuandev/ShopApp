using OpenIddictAPI;
using OpenIddictAPI.ConfigurationOptions;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

var appSettings = new AppSettings();

var configuration = builder.Configuration;
configuration.Bind(appSettings);

// Add services to the container.
builder.Services.AddOpenIddictServices(builder.Configuration, appSettings);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    await app.InitialiseDatabaseAsync(appSettings);
}

app.UseCors("AllowAllOrigins");

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
