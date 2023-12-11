using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
namespace PriceNegotiationAPI.Application.Configuration;

public static class ServiceCollection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg => { cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()); });
        
        return services;
    }
}