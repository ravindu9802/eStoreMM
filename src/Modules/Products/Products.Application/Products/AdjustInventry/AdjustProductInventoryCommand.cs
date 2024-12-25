using MediatR;
using Products.Domain.Enums;

namespace Products.Application.Products.AdjustInventry;

public record AdjustProductInventoryCommand(Guid ProductId, int Quantity, AdjustInventoryOptions Option)
    : IRequest<bool>;