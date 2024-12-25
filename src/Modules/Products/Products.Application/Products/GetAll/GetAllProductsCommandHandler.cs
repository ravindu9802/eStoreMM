using MediatR;
using Microsoft.Extensions.Logging;
using Products.Domain.Entities;
using Products.Domain.Repositories;

namespace Products.Application.Products.GetAll;

public class GetAllProductsCommandHandler : IRequestHandler<GetAllProductsQuery, List<Product>?>
{
    private readonly ILogger<GetAllProductsCommandHandler> _logger;
    private readonly IProductRepository _repository;

    public GetAllProductsCommandHandler(IProductRepository repository, ILogger<GetAllProductsCommandHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<List<Product>?> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting request {@RequestName}, {@DatetimeUtc}", request, DateTime.UtcNow);

        var res = await _repository.GetAllProductsAsync(cancellationToken);
        _logger.LogInformation("Products fetched. {@RequestName}, {@DatetimeUtc}", request, DateTime.UtcNow);

        return res;
    }
}