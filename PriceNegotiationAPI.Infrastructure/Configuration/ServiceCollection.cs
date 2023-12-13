using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using PriceNegotiationAPI.Domain.Repository;
using PriceNegotiationAPI.Infrastructure.Exception;
using PriceNegotiationAPI.Infrastructure.Repository;

namespace PriceNegotiationAPI.Infrastructure.Configuration;

public static class ServiceCollection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services
            .AddScoped<IProductRepository, ProductRepository>()
            .AddScoped<IUserRepository, UserRepository>()
            .AddScoped<ExceptionHandlingMiddleware>()
            .AddScoped<INegotiationRepository, NegotiationRepository>();
        
        return services;
    }
}