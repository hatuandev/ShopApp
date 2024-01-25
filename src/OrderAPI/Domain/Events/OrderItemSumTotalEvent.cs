using OrderAPI.Domain.Common;

namespace OrderAPI.Domain.Events;

public class OrderItemSumTotalEvent : BaseEvent
{
    public OrderItemSumTotalEvent(OrderItem item)
    {
        Item = item;
    }

    public OrderItem Item { get; }
}
