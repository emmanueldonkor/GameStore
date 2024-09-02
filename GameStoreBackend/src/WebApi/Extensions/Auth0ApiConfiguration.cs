using System.Security.Claims;
using Application.Authorization;
using Domain.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;

namespace WebApi.Extensions;

public static class Auth0ApiConfiguration
{
    private static readonly List<string> scopes =
         ["delete:games", "update:games", "create:games", "read:orders","read:user-orders", "update:orders", "create:checkout"];
   
    public static IServiceCollection ConfigureAuth0(this IServiceCollection services, IConfiguration configuration,string domain)
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
        foreach (var scope in scopes)
        {
            services.AddAuthorizationBuilder()
                .AddPolicy(scope, policy =>
                    policy.Requirements.Add(new HasScopeRequirement(scope, domain)));
        }
        services.AddSingleton<IAuthorizationHandler,HasScopeHandler>();
        return services;
    }
}