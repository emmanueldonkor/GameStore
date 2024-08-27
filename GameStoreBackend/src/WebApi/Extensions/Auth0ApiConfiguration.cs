using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace WebApi.Extensions;

public static class Auth0ApiConfiguration
{
    public static IServiceCollection ConfigureAuth0(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.Authority = $"https://{configuration["Auth0:Domain"]}/";
            options.Audience = configuration["Auth0:Audience"];
            options.TokenValidationParameters = new TokenValidationParameters
            {
                NameClaimType = ClaimTypes.NameIdentifier
            };
        });
        services.AddAuthorization();
       services.AddAuthorizationBuilder()
            .AddPolicy("User", policy =>policy.RequireRole("User"))
            .AddPolicy("Admin", policy => policy.RequireRole("Admin")); 
            
       
        return services;
    }
}