using OrderAPI.Application.Orders.Commands.CreateOrder;
using OrderAPI.Application.Orders.Commands.DeleteOrder;
using OrderAPI.Application.Orders.Commands.UpdateOrder;
using OrderAPI.Application.Orders.Queries.GetOrderWithPagination;
using OrderAPI.Infrastructure.BaseEndpoint;

namespace OrderAPI.Endpoints;

public class Orders : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        var group = app.MapGroup("/api/v1/orders");

        group.MapGet("/", GetOrderWithPagination);
        group.MapPost("/", CreateOrder);
        group.MapPut("/{id}", UpdateOrder);
        group.MapDelete("/{id}", DeleteOrder);
    }

    public async Task<PaginatedList<GetOrderWithPaginationDto>> GetOrderWithPagination(ISender sender, [AsParameters] GetOrderWithPaginationQuery query)
    {
        return await sender.Send(query);
    }
    
    public async Task<int> CreateOrder(ISender sender, CreateOrderCommand command)
    {
        return await sender.Send(command);
    }

    public async Task<IResult> UpdateOrder(ISender sender, int id, UpdateOrderCommand command)
    {
        if (id != command.Id) return Results.BadRequest();
        await sender.Send(command);
        return Results.NoContent();
    }

    public async Task<IResult> DeleteOrder(ISender sender, int id)
    {
        await sender.Send(new DeleteOrderCommand(id));
        return Results.NoContent();
    }
}

