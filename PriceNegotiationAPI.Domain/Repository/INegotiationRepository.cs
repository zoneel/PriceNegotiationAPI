using PriceNegotiationAPI.Domain.Entities;

namespace PriceNegotiationAPI.Domain.Repository;

public interface INegotiationRepository
{
    Task<IEnumerable<Negotiation>> GetAllNegotiationsAsync(CancellationToken ct = default);
    Task<Negotiation> GetNegotiationByIdAsync(int negotiationId, CancellationToken ct = default);
    Task AddNegotiationAsync(Negotiation negotiation, CancellationToken ct = default);
    Task DeleteNegotiationAsync(int negotiationId, CancellationToken ct = default);
    Task AcceptNegotiationAsync(int negotiationId, CancellationToken ct = default);
    Task DeclineNegotiationAsync(int negotiationId, CancellationToken ct = default);
    Task<bool> CheckIfUserHasPendingNegotiationForProduct(int userId, int ProductId);
}