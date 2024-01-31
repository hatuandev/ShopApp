using ProductAPI.Application.StockPickings.Commands.CreateStockPicking;
using ProductAPI.Application.StockPickings.Queries.GetStockPickingById;
using ProductAPI.Application.StockPickings.Queries.GetStockPickingWithPagination;
using ProductAPI.Infrastructure.BaseEndpoint;

namespace ProductAPI.Endpoints;

public class StockPickings : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        var group = app.MapGroup("/api/v1/stock-pickings");

        group.MapGet("/", GetStockPickingWithPagination);
        group.MapGet("/{id:int}", GetStockPickingById);
        group.MapPost("/", CreateStockPicking);
    }

    public async Task<PaginatedList<GetStockPickingWithPaginationDto>> GetStockPickingWithPagination(ISender sender, [AsParameters] GetStockPickingWithPaginationQuery query)
    {
        return await sender.Send(query);
    }

    public async Task<GetStockPickingByIdDto> GetStockPickingById(ISender sender, [AsParameters] GetStockPickingByIdQuery query)
    {
        return await sender.Send(query);
    }

    
    public async Task<int> CreateStockPicking(ISender sender, CreateStockPickingCommand command)
    {
        return await sender.Send(command);
    }
}
