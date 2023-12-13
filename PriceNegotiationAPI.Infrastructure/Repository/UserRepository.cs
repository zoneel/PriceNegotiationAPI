using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using PriceNegotiationAPI.Domain.Entities;
using PriceNegotiationAPI.Domain.Repository;

namespace PriceNegotiationAPI.Infrastructure.Repository;

    public class UserRepository : IUserRepository
    {
        private readonly IConfiguration _configuration;

        public UserRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private IDbConnection Connection => new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

        public async Task<IEnumerable<User>> GetAllUsersAsync(CancellationToken ct = default)
        {
            using IDbConnection conn = Connection;
            conn.Open();
            var users = await conn.QueryAsync<User>("SELECT * FROM Users");
            return users;
        }

        public async Task<User> GetUserByIdAsync(int userId, CancellationToken ct = default)
        {
            using IDbConnection conn = Connection;
            conn.Open();
            var user = await conn.QueryFirstOrDefaultAsync<User>("SELECT * FROM Users WHERE Id = @UserId", new { UserId = userId });
            return user;
        }

        public async Task AddUserAsync(User user, CancellationToken ct = default)
        {
            using IDbConnection conn = Connection;
            conn.Open();
            await conn.ExecuteAsync("INSERT INTO Users (Name, Email, Password, Role) VALUES (@Name, @Email, @Password, @Role)", new { user.Name, user.Email, user.Password, user.Role });
        }

        public async Task DeleteUserAsync(int userId, CancellationToken ct = default)
        {
            using IDbConnection conn = Connection;
            conn.Open();
            await conn.ExecuteAsync("DELETE FROM Users WHERE Id = @UserId", new { UserId = userId });
        }

        public async Task<User> GetUserByEmailAsync(string email, CancellationToken ct = default)
        {
            using IDbConnection conn = Connection;
            conn.Open();
            var user = await conn.QueryFirstOrDefaultAsync<User>("SELECT * FROM Users WHERE Email = @Email", new { Email = email });
            return user;
        }

        public async Task<User> GetUserByNameAsync(string name, CancellationToken ct = default)
        {
            using IDbConnection conn = Connection;
            conn.Open();
            var user = await conn.QueryFirstOrDefaultAsync<User>("SELECT * FROM Users WHERE Name = @Name", new { Name = name });
            return user;
        }
    }
