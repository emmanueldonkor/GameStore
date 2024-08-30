using Microsoft.AspNetCore.Mvc;

namespace WebApi.Extensions;

public static class ApiVersionConfiguration
{
    public static IServiceCollection ConfigureApiVersion(this IServiceCollection services)
    {
        services.AddApiVersioning(options =>
        {
            options.ReportApiVersions = true;
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.DefaultApiVersion = new ApiVersion(1, 0);

        });
        
        return services;
    }
}