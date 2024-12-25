using Products.Domain.Enums;

namespace Products.Application.Products.Add;

public record ProductRequest(string Name, ProductCategory Category, int Quantity, double Amount, string Currency);