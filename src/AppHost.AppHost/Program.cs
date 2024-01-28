var builder = DistributedApplication.CreateBuilder(args);

var redis = builder.AddRedisContainer("redis");
var sqlserver = builder.AddSqlServerContainer("sqlserver");

var IdentityServerDb = sqlserver.AddDatabase("IdentityServerDb");
var ProductDb = sqlserver.AddDatabase("ProductDb");
var OrderDb = sqlserver.AddDatabase("OrderDb");

// Services
builder.AddProject<Projects.OpenIddictAPI>("openiddictapi")
       .WithReference(IdentityServerDb);

builder.AddProject<Projects.ProductAPI>("productapi")
       .WithReference(ProductDb);

builder.AddProject<Projects.OrderAPI>("orderapi")
       .WithReference(OrderDb);

builder.Build().Run();
