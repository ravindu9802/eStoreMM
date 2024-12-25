using SharedKernel.Primitives;
using SharedKernel.ValueObjects;

namespace Orders.Domain.Entities;

public class LineItem : Entity
{
    private LineItem(Guid id) : base(id)
    {
    }

    private LineItem(Guid id, Guid productId, int count, Price price) : base(id)
    {
        ProductId = productId;
        Price = price;
        Count = count;
    }

    public Guid ProductId { get; private set; }
    public int Count { get; private set; }
    public Price Price { get; private set; }

    public static LineItem Create(Guid productId, int count, Price price)
    {
        if (count < 1) throw new ArgumentException("Count should be a positive integer.", nameof(count));

        return new LineItem(Guid.NewGuid(), productId, count, price);
    }
}