using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Infrastructure.Persistence;
using Application.Interfaces;


namespace Infrastructure;

public static  class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
      var connString = configuration.GetConnectionString("DefaultConnection");

      services.AddSqlServer<ApplicationDbContext>(connString, builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName));

      services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
      
      return services;
    }
}