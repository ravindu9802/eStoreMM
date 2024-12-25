using MediatR;
using SharedKernel.ValueObjects;

namespace Products.Application.Products.AdjustPrice;

public record AdjustProductPriceCommand(Guid ProductId, Price NewPrice) : IRequest<bool>;