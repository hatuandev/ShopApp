namespace OrderAPI.Application.Orders.Queries.GetOrderWithPagination;

public class GetOrderWithPaginationDto
{
    public string? Name { get; init; }
    public DateTimeOffset DateOrder { get; init; }
    public decimal Total { get; init; }
    public List<OrderItem> OrderItems { get; init; } = new List<OrderItem>();

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Order, GetOrderWithPaginationDto>();
        }
    }
}


