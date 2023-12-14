using Serilog;

namespace PriceNegotiationAPI.Configuration;

public static class ServiceCollection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        #region Serilog
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();
        
        services.AddLogging(loggingBuilder =>
        {
            loggingBuilder.AddSerilog();
        });
        #endregion
        
        services.AddAuthorization(options =>
        {
            options.AddPolicy("EmployeeOnlyPolicy", policy =>
            {
                policy.RequireClaim("Role", "1");
            });
        });
        
        return services;
    }
}