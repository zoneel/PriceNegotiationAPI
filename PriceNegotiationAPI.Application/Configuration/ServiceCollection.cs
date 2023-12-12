using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PriceNegotiationAPI.Application.Product.Dto;
using PriceNegotiationAPI.Application.Validators;

namespace PriceNegotiationAPI.Application.Configuration;

public static class ServiceCollection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg => { cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()); });

        services
            .AddTransient<IValidator<ProductDto>, ProductDtoValidator>()
            .AddTransient<IValidator<int>, IntValidator>();
        
        return services;
    }
}