using Microsoft.AspNetCore.Http;
using PriceNegotiationAPI.Application.Abstraction;
using PriceNegotiationAPI.Application.Negotiation.Command.Accept;
using PriceNegotiationAPI.Application.Negotiation.Mapping;
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
        var pendingNegotiation = await _negotiationRepository.CheckIfUserHasPendingNegotiationForProduct(request.userId, request.dto.ProductId);
        
        if (pendingNegotiation)
        {
            throw new IdempotencyException("User already has a pending negotiation for this product.");
        }
        
        var product = await _productRepository.GetProductByIdAsync(request.dto.ProductId);
        
        if(request.dto.ProposedPrice > (product.BasePrice * 2))
        {
            throw new HighballOfferException("Price offered cannot be two times greater than base price");
        }
        
        var negotiation = NegotiationMapping.MapAddNegotiationDtotoNegotiationEntity(request.dto, request.userId);
        await _negotiationRepository.AddNegotiationAsync(negotiation, cancellationToken);
    }
}