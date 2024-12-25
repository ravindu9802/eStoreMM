using System.Collections.ObjectModel;
using Orders.Domain.Enums;
using SharedKernel.Primitives;

namespace Orders.Domain.Entities;

public class Order : AggregateRoot
{
    private Order(Guid id) : base(id)
    {
    }

    private Order(Guid id, Guid customer, Collection<LineItem> lineItems, OrderStatus status) : base(id)
    {
        Customer = customer;
        LineItems = lineItems;
        Status = status;
        CreatedAtUtc = DateTime.UtcNow;
    }

    public Guid Customer { get; private set; }
    public Collection<LineItem> LineItems { get; private set; }
    public OrderStatus Status { get; private set; }
    public DateTime CreatedAtUtc { get; private set; }

    public static Order Create(Guid customer, Collection<LineItem> lineItems)
    {
        if (!lineItems.Any()) new ArgumentException("Lineitems cannot be empty.", nameof(lineItems));

        return new Order(Guid.NewGuid(), customer, lineItems, OrderStatus.Pending);
    }

    public Order ChangeOrderStatus(OrderStatus status)
    {
        if (Status == OrderStatus.Completed || Status == OrderStatus.Cancelled)
            throw new ArgumentException($"Invalid order status change from {nameof(Status)} to {nameof(status)}");
        Status = status;
        return this;
    }
}