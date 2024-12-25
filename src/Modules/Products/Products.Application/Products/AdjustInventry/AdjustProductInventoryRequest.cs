using Products.Domain.Enums;

namespace Products.Application.Products.AdjustInventry;

public record AdjustProductInventoryRequest(Guid ProductId, int Quantity, AdjustInventoryOptions Option);