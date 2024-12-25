using MediatR;

namespace Products.Application.Products.Delete;

public record DeleteProductCommand(Guid Id) : IRequest<bool>;