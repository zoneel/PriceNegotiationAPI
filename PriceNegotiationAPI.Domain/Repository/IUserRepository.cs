using PriceNegotiationAPI.Domain.Entities;

namespace PriceNegotiationAPI.Domain.Repository;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllUsersAsync(CancellationToken ct = default);
    Task<User> GetUserByIdAsync(int userId, CancellationToken ct = default);
    Task AddUserAsync(User user, CancellationToken ct = default);
    Task DeleteUserAsync(int userId, CancellationToken ct = default);
    Task<User> GetUserByEmailAsync(string email, CancellationToken ct = default);
    Task<User> GetUserByNameAsync(string name, CancellationToken ct = default);
}