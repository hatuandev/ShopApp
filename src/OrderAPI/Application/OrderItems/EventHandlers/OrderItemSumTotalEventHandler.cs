using OrderAPI.Domain.Events;

namespace OrderAPI.Application.OrderItems.EventHandlers;

public class OrderItemSumTotalEventHandler : INotificationHandler<OrderItemSumTotalEvent>
{
    private readonly ILogger<OrderItemSumTotalEvent> _logger;
    private readonly IGenericRepository<Order> _context;

    public OrderItemSumTotalEventHandler(ILogger<OrderItemSumTotalEvent> logger, IGenericRepository<Order> context)
    {
        _logger = logger;
        _context = context;
    }

    public Task Handle(OrderItemSumTotalEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Domain Event: {DomainEvent}", notification.GetType().Name);
        var order = _context.GetById(notification.Item.OrderId);
        if (order != null)
            order.Total += notification.Item.Price != null ? (decimal)notification.Item.Price : 0;

        return Task.CompletedTask;
    }
}
