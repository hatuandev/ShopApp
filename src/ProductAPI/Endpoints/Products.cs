using ProductAPI.Application.Products.Commands.CreateProduct;
using ProductAPI.Application.Products.Commands.DeleteProduct;
using ProductAPI.Application.Products.Commands.UpdateProduct;
using ProductAPI.Application.Products.Queries.GetProductWithPagination;
using ProductAPI.Infrastructure.BaseEndpoint;

namespace ProductAPI.Endpoints;

public class Products : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        var group = app.MapGroup("/api/v1/products");

        group.MapGet("/", GetProductWithPagination);
        group.MapPost("/", CreateProduct);
        group.MapPut("/{id}", UpdateProduct);
        group.MapDelete("/{id}", DeleteProduct);
    }

    public async Task<PaginatedList<GetProductWithPaginationDto>> GetProductWithPagination(ISender sender, [AsParameters] GetProductWithPaginationQuery query)
    {
        return await sender.Send(query);
    }

    public async Task<int> CreateProduct(ISender sender, CreateProductCommand command)
    {
        return await sender.Send(command);
    }

    public async Task<IResult> UpdateProduct(ISender sender, int id, UpdateProductCommand command)
    {
        if (id != command.Id) return Results.BadRequest();
        await sender.Send(command);
        return Results.NoContent();
    }

    public async Task<IResult> DeleteProduct(ISender sender, int id)
    {
        await sender.Send(new DeleteProductCommand(id));
        return Results.NoContent();
    }
}


