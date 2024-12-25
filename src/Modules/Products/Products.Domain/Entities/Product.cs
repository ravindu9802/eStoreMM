using Products.Domain.Enums;
using SharedKernel.Primitives;
using SharedKernel.ValueObjects;

namespace Products.Domain.Entities;

public sealed class Product : Entity
{
    private Product(Guid id) : base(id)
    {
    }

    private Product(Guid id, string name, ProductCategory category, int quantity, Price price) : base(id)
    {
        Name = name;
        Category = category;
        Quantity = quantity;
        Price = price;
    }

    public string Name { get; private set; }
    public ProductCategory Category { get; private set; }
    public int Quantity { get; private set; }
    public Price Price { get; private set; }
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public Guid? CreatedBy { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public Guid? UpdatedBy { get; private set; }

    public static Product Create(Guid id, string name, ProductCategory category, int quantity, Price price)
    {
        if (quantity < 0) throw new ArgumentException("Quantity should be a positive integer.", nameof(quantity));

        var product = new Product(Guid.NewGuid(), name, category, quantity, price);
        return product;
    }

    public bool AdjustInventory(string reason, int quantity, AdjustInventoryOptions option)
    {
        if (string.IsNullOrWhiteSpace(reason)) throw new ArgumentException("Reason cannot be empty.", nameof(reason));

        if (quantity < 0) throw new ArgumentException("Quantity should be a positive integer.", nameof(quantity));

        if (option is AdjustInventoryOptions.Substract)
        {
            if (Quantity - quantity < 0)
                new ArgumentException($"{nameof(option)} operation failed. Not enough items in the inventory.",
                    nameof(quantity));

            Quantity = Quantity - quantity;
        }

        Quantity = Quantity + quantity;
        return true;
    }
}