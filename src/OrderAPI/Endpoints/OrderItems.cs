using OrderAPI.Application.OrderItems.Commands.CreateOrderItem;
using OrderAPI.Infrastructure.BaseEndpoint;

namespace OrderAPI.Endpoints;

public class OrderItems : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        var group = app.MapGroup("/api/v1/order-items");
        group.MapPost("/", CreateOrderItem);
    }

    public async Task<int> CreateOrderItem(ISender sender, CreateOrderItemCommand command)
    {
        return await sender.Send(command);
    }
}
