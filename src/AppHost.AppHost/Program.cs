using Microsoft.Extensions.Configuration;

var builder = DistributedApplication.CreateBuilder(args);

// Services
builder.AddProject<Projects.OpenIddictAPI>("openiddictapi");

builder.AddProject<Projects.ProductAPI>("productapi");

builder.AddProject<Projects.OrderAPI>("orderapi");

builder.AddProject<Projects.GatewayApi>("gatewayapi");

builder.Build().Run();
