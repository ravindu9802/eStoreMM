using MediatR;
using Products.Domain.Repositories;

namespace Products.Application.Products.AdjustInventry;

internal sealed class AdjustProductInventoryCommandHandler : IRequestHandler<AdjustProductInventoryCommand, bool>
{
    private readonly IProductRepository _repository;
    private readonly IProductUoW _unitOfWork;

    public AdjustProductInventoryCommandHandler(IProductRepository repository, IProductUoW unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(AdjustProductInventoryCommand request, CancellationToken cancellationToken)
    {
        var product = await _repository.GetProductByIdAsync(request.ProductId);

        if (product is null) return false;

        if (product.AdjustInventory("Test DDD", request.Quantity, request.Option))
        {
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }

        ;

        return false;
    }
}