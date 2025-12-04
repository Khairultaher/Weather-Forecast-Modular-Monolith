using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MMA.Security.Persistence;

namespace MMA.Security;

public static class SecurityModule {
    public static IServiceCollection AddSecurityModule(
        this IServiceCollection services,
        IConfiguration configuration
        ) {

        // Register any services specific to the Weather module here
        // Read connection string
        var connectionString = configuration.GetConnectionString("SecurityDbConnection");

        // Register the DbContext
        services.AddDbContext<SecurityDbContext>(options => {
            options.UseSqlServer(connectionString, sql => {
                sql.MigrationsAssembly("MMA.Security"); // important
            });

            // EF Core 10 lazy loading
            options.UseLazyLoadingProxies(true);
        });

        services.AddScoped<ISecurityDbContext>(provider => provider.GetRequiredService<SecurityDbContext>());


        return services;
    }
}
