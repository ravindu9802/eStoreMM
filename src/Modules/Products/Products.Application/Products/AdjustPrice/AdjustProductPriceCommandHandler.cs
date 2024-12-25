using MediatR;

namespace Products.Application.Products.AdjustPrice;

public sealed class AdjustProductPriceCommandHandler : IRequestHandler<AdjustProductPriceCommand, bool>
{
    public Task<bool> Handle(AdjustProductPriceCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}