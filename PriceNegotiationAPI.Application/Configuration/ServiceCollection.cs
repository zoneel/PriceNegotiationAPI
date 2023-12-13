using System.Reflection;
using System.Text;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using PriceNegotiationAPI.Application.Negotiation.Dto;
using PriceNegotiationAPI.Application.Product.Dto;
using PriceNegotiationAPI.Application.Security;
using PriceNegotiationAPI.Application.User.Dto;
using PriceNegotiationAPI.Application.Validators;
using PriceNegotiationAPI.Domain.Security;
using PriceNegotiationAPI.Infrastructure.Security;

namespace PriceNegotiationAPI.Application.Configuration;

public static class ServiceCollection
{
    private const string AuthOptionsSectionName = "AuthOptions";
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(cfg => { cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()); });

        services.Configure<AuthOptions>(configuration.GetRequiredSection(AuthOptionsSectionName));
        services.AddAuthentication(o =>
        {
            o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        });
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                var authOptions = new AuthOptions()
                {
                    Audience = configuration.GetSection(AuthOptionsSectionName).GetSection("Audience").Value,
                    Issuer = configuration.GetSection(AuthOptionsSectionName).GetSection("Issuer").Value,
                    SigningKey = configuration.GetSection(AuthOptionsSectionName).GetSection("SigningKey").Value,
                    Expiry = TimeSpan.FromHours(1)
                };


                options.Audience = authOptions.Audience;
                options.IncludeErrorDetails = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = authOptions.Issuer,
                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authOptions.SigningKey))
                };
            });

        services
            .AddTransient<IValidator<AddProductDto>, AddProductDtoValidator>()
            .AddTransient<IValidator<int>, IntValidator>()
            .AddTransient<IValidator<LoginUserDto>, LoginUserDtoValidator>()
            .AddTransient<IValidator<RegisterUserDto>, RegisterUserDtoValidator>()
            .AddTransient<IValidator<AddNegotiationDto>, AddNegotiationDtoValidator>()
            .AddSingleton<IPasswordHasher<Domain.Entities.User>, PasswordHasher<Domain.Entities.User>>()
            .AddTransient<IPasswordManager, PasswordManager>()
            .AddTransient<IJwtService, JwtService>();
        
        return services;
    }
}