using MediatR;
using Products.Domain.Entities;

namespace Products.Application.Products.GetAll;

public record GetAllProductsQuery : IRequest<List<Product>?>;