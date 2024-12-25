using Mapster;
using Products.Application.Products.Add;
using Products.Domain.Entities;
using SharedKernel.ValueObjects;

namespace Products.Application.Mappings;

public class ProductMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        TypeAdapterConfig<ProductRequest, Product>
            .NewConfig()
            .ConstructUsing(src => Product.Create(
                Guid.NewGuid(),
                src.Name,
                src.Category,
                src.Quantity,
                Price.Create(src.Amount, src.Currency)
            ));
    }
}