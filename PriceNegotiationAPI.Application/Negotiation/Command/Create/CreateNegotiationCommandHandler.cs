using Microsoft.AspNetCore.Http;
using PriceNegotiationAPI.Application.Abstraction;
using PriceNegotiationAPI.Application.Negotiation.Command.Accept;
using PriceNegotiationAPI.Application.Negotiation.Mapping;
using PriceNegotiationAPI.Domain.Enums;
using PriceNegotiationAPI.Domain.Exceptions;
using PriceNegotiationAPI.Domain.Repository;

namespace PriceNegotiationAPI.Application.Negotiation.Command.Create;

internal class CreateNegotiationCommandHandler : ICommandHandler<CreateNegotiationCommand>
{
    private readonly INegotiationRepository _negotiationRepository;
    private readonly IProductRepository _productRepository;

    public CreateNegotiationCommandHandler(INegotiationRepository negotiationRepository, IProductRepository productRepository)
    {
        _negotiationRepository = negotiationRepository;
        _productRepository = productRepository;
    }
    public async Task Handle(CreateNegotiationCommand request, CancellationToken cancellationToken)
    {
        var userNegotiationForProduct = await _negotiationRepository.GetUserNegotiationForProduct(request.userId, request.dto.ProductId);

        if (userNegotiationForProduct == null)
        {
            var negotiationtoAdd = NegotiationMapping.MapAddNegotiationDtotoNegotiationEntity(request.dto, request.userId);
            _negotiationRepository.AddNegotiationAsync(negotiationtoAdd, cancellationToken);
            return;
        }
        
        if(userNegotiationForProduct.Status == OfferState.Accepted)
            throw new NegotiationAlreadyAcceptedException("Negotiation already accepted. Once accepted, it cannot be rejected by this endpoint.");
        
        if(userNegotiationForProduct.UserAttempts >= 3)
            throw new TooManyAttemptsException("User has reached maximum number of attempts for this product.");
        
        var product = await _productRepository.GetProductByIdAsync(request.dto.ProductId);
        if(request.dto.ProposedPrice > (product.BasePrice * 2))
            throw new HighballOfferException("Price offered cannot be two times greater than base price");
        
        
        var negotiation = NegotiationMapping.MapAddNegotiationDtotoNegotiationEntity(request.dto, request.userId);
        await _negotiationRepository.BumpNegotiationAsync(userNegotiationForProduct.NegotiationId, negotiation.ProposedPrice, userNegotiationForProduct.UserAttempts, cancellationToken);
    }
}