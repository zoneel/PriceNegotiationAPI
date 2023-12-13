using PriceNegotiationAPI.Application.Configuration;
using PriceNegotiationAPI.Configuration;
using PriceNegotiationAPI.Infrastructure.Configuration;
using PriceNegotiationAPI.Infrastructure.Exception;

namespace PriceNegotiationAPI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services
            .AddSwaggerGen()
            .AddApplication(builder.Configuration)
            .AddInfrastructure()
            .AddPresentation()
            .AddEndpointsApiExplorer()
            .AddControllers();
        
        var app = builder.Build();
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseMiddleware<ExceptionHandlingMiddleware>();
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}