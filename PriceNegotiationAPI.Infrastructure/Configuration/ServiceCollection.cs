using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using PriceNegotiationAPI.Domain.Repository;
using PriceNegotiationAPI.Infrastructure.Repository;

namespace PriceNegotiationAPI.Infrastructure.Configuration;

public static class ServiceCollection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services
            .AddScoped<IProductRepository, ProductRepository>();
        
        return services;
    }
}