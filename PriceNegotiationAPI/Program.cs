using PriceNegotiationAPI.Application.Configuration;
using PriceNegotiationAPI.Configuration;
using PriceNegotiationAPI.Infrastructure.Configuration;

namespace PriceNegotiationAPI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        builder.Services
            .AddSwaggerGen()
            .AddApplication()
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

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}