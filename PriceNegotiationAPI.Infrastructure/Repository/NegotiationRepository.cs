using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using PriceNegotiationAPI.Domain.Entities;
using PriceNegotiationAPI.Domain.Repository;

namespace PriceNegotiationAPI.Infrastructure.Repository;

 public class NegotiationRepository : INegotiationRepository
    {
        private readonly IConfiguration _configuration;

        public NegotiationRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private IDbConnection Connection => new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        public async Task<List<Negotiation>> GetAllNegotiationsAsync(CancellationToken ct = default)
        {
            using IDbConnection conn = Connection;
            conn.Open();
            var negotiations = await conn.QueryAsync<Negotiation>("SELECT * FROM Negotiations");
            return new List<Negotiation>(negotiations);
        }
        
        public async Task<List<Negotiation>> GetAllPendingNegotiationsAsync(CancellationToken ct = default)
        {
            using IDbConnection conn = Connection;
            conn.Open();
            var negotiations = await conn.QueryAsync<Negotiation>("SELECT * FROM Negotiations WHERE Status = 0");
            return new List<Negotiation>(negotiations);
        }

        public async Task<Negotiation> GetNegotiationByIdAsync(int negotiationId, CancellationToken ct = default)
        {
            using IDbConnection conn = Connection;
            conn.Open();
            var negotiation = await conn.QueryFirstOrDefaultAsync<Negotiation>("SELECT * FROM Negotiations WHERE NegotiationId = @NegotiationId", new { NegotiationId = negotiationId });
            return negotiation;
        }

        public async Task AddNegotiationAsync(Negotiation negotiation, CancellationToken ct = default)
        {
            using IDbConnection conn = Connection;
            conn.Open();
            await conn.ExecuteAsync("INSERT INTO Negotiations (ProductId, ProposedPrice, UserAttempts, Status, CreatorUserId) VALUES (@ProductId, @ProposedPrice, @UserAttempts, @Status, @CreatorUserId)", new { negotiation.ProductId, negotiation.ProposedPrice, negotiation.UserAttempts, negotiation.Status, negotiation.CreatorUserId });
        }

        public async Task DeleteNegotiationAsync(int negotiationId, CancellationToken ct = default)
        {
            using IDbConnection conn = Connection;
            conn.Open();
            await conn.ExecuteAsync("DELETE FROM Negotiations WHERE Id = @NegotiationId", new { NegotiationId = negotiationId });
        }
        
        public async Task AcceptNegotiationAsync(int negotiationId, CancellationToken ct = default)
        {
            using IDbConnection conn = Connection;
            conn.Open();
            await conn.ExecuteAsync("UPDATE Negotiations SET Status = @Status WHERE NegotiationId = @NegotiationId", new { NegotiationId = negotiationId, Status = 1 });
        }
        
        public async Task DeclineNegotiationAsync(int negotiationId, int currentAttempts, CancellationToken ct = default)
        {
            currentAttempts = currentAttempts + 1;
            using IDbConnection conn = Connection;
            conn.Open();
            await conn.ExecuteAsync("UPDATE Negotiations SET Status = @Status, UserAttempts = @UserAttempts WHERE NegotiationId = @NegotiationId", new { NegotiationId = negotiationId, Status = 2, UserAttempts = currentAttempts });
        }

        public async Task<Negotiation> GetUserNegotiationForProduct(int userId, int ProductId)
        {
            using IDbConnection conn = Connection;
            conn.Open();
            var negotiation = await conn.QueryFirstOrDefaultAsync<Negotiation>
            ("SELECT * FROM Negotiations WHERE CreatorUserId = @CreatorUserId AND ProductId = @ProductId",
                new { CreatorUserId = userId, ProductId = ProductId});

            return negotiation;
        }
        
        //bump negotiation. If your negotiation got rejected you can bump it with new better price, up to three times.
        //if you bump it three times and it gets rejected again, you can't bump it anymore.
        public async Task BumpNegotiationAsync(int negotiationId, decimal newPrice, int currentAttempts, CancellationToken ct = default)
        {
            currentAttempts = currentAttempts + 1;
            using IDbConnection conn = Connection;
            conn.Open();
            await conn.ExecuteAsync("UPDATE Negotiations SET ProposedPrice = @ProposedPrice, Status = @Status WHERE NegotiationId = @NegotiationId", new { NegotiationId = negotiationId, ProposedPrice = newPrice, Status = 0 });
        }
    }