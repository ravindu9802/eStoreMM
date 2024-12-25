using MediatR;
using Microsoft.Extensions.Logging;
using Products.Domain.Entities;
using Products.Domain.Repositories;
using SharedKernel.ValueObjects;

namespace Products.Application.Products.Add;

public sealed class AddProductCommandHandler : IRequestHandler<AddProductCommand, Guid>
{
    private readonly ILogger<AddProductCommandHandler> _logger;
    private readonly IProductRepository _repository;
    private readonly IProductUoW _unitOfWork;

    public AddProductCommandHandler(IProductRepository repository, IProductUoW unitOfWork,
        ILogger<AddProductCommandHandler> logger)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Guid> Handle(AddProductCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting request {@RequestName}, {@DatetimeUtc}", request, DateTime.UtcNow);

        var req = request.ProductRequest;
        var price = Price.Create(req.Amount, req.Currency);

        _logger.LogInformation("Price created. {@Price}, {@DatetimeUtc}", price, DateTime.UtcNow);

        var product = Product.Create(Guid.NewGuid(), req.Name, req.Category, req.Quantity, price);
        _logger.LogInformation("Product created. {@Product}, {@DatetimeUtc}", product, DateTime.UtcNow);

        var res = await _repository.AddProductAsync(product, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        _logger.LogInformation("Product persisted. {@ProductId}, {@Product}, {@DatetimeUtc}", res, product,
            DateTime.UtcNow);

        return res;
    }
}