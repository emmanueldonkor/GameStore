using Microsoft.OpenApi.Models;

namespace WebApi.Extensions;

public static class SwaggerConfiguration
{
    private static readonly string[] value = ["Bearer"];
    public static IServiceCollection ConfigureSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
      {
          c.SwaggerDoc("v1", new OpenApiInfo { Title = "GameStoreApi", Version = "v1.0.0" });
          var securitySchema = new OpenApiSecurityScheme
          {
              Description = "Using the Authorization header with the Bearer scheme.",
              Name = "Authorization",
              In = ParameterLocation.Header,
              Type = SecuritySchemeType.Http,
              Scheme = "bearer",
              Reference = new OpenApiReference
              {
                  Type = ReferenceType.SecurityScheme,
                  Id = "Bearer"
              }
          };

          c.AddSecurityDefinition("Bearer", securitySchema);

          c.AddSecurityRequirement(new OpenApiSecurityRequirement
          {
              { securitySchema, value }
          });
      });
      return services;
    }
}

        
    
