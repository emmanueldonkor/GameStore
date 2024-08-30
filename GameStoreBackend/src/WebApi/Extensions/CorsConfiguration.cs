namespace WebApi.Extensions
{
    public static class CorsConfiguration
    {
        public static IServiceCollection ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("GameStorePolicy",
                    builder => builder
                        .WithOrigins("https://localhost:3000")
                        .WithMethods("GET", "POST", "PUT", "DELETE")
                        .AllowAnyHeader());
            });
            return services;
        }
    }
}