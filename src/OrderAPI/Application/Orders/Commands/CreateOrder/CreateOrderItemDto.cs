namespace OrderAPI.Application.Orders.Commands.CreateOrder;

public class CreateOrderItemDto
{
    public int ProductId { get; init; }
    public decimal? Quantity { get; init; }
    public decimal? Price { get; init; }
}
