using ProductAPI.Application.ProductTypes.Commands.CreateProductType;
using ProductAPI.Application.ProductTypes.Commands.DeleteProductType;
using ProductAPI.Application.ProductTypes.Commands.UpdateProductType;
using ProductAPI.Application.ProductTypes.Queries.GetProductTypeAll;
using ProductAPI.Infrastructure.BaseEndpoint;

namespace ProductAPI.Endpoints;

public class ProductTypes : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        var group = app.MapGroup("/api/v1/product-types");

        group.MapGet("/", GetProductTypeAll);
        group.MapPost("/", CreateProductType);
        group.MapPut("/{id}", UpdateProductType);
        group.MapDelete("/{id}", DeleteProductType);
    }

    public async Task<IEnumerable<GetProductTypeAllDto>> GetProductTypeAll(ISender sender, [AsParameters] GetProductTypeAllQuery query)
    {
        return await sender.Send(query);
    }

    public async Task<int> CreateProductType(ISender sender, CreateProductTypeCommand command)
    {
        return await sender.Send(command);
    }

    public async Task<IResult> UpdateProductType(ISender sender, int id, UpdateProductTypeCommand command)
    {
        if (id != command.Id) return Results.BadRequest();
        await sender.Send(command);
        return Results.NoContent();
    }

    public async Task<IResult> DeleteProductType(ISender sender, int id)
    {
        await sender.Send(new DeleteProductTypeCommand(id));
        return Results.NoContent();
    }
}