using MediatR;
using Products.Domain.Repositories;

namespace Products.Application.Products.Delete;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, bool>
{
    private readonly IProductRepository _repository;

    public DeleteProductCommandHandler(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        return await _repository.DeleteProductAsync(request.Id);
    }
}