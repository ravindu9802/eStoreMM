using MediatR;

namespace Products.Application.Products.Add;

public record AddProductCommand(ProductRequest ProductRequest) : IRequest<Guid>;