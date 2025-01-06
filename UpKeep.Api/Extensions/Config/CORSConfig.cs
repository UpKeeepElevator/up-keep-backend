namespace UpKeepApi.Extensions.Config;

public static class CORSConfig
{
    public static void ConfigurarCORS(this IServiceCollection services, string MyAllowSpecifiOrigins)
    {
        string[] domains =
        {
            "http://localhost:8100",
            "http://localhost:4200",
        };


        services.AddCors(options =>
        {
            options.AddPolicy(name: MyAllowSpecifiOrigins,
                policy =>
                {
                    policy.WithOrigins(domains)
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
        });
    }
}