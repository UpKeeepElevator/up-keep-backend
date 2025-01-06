using UpKeep.Data.Configuration;
using UpKeepApi.Extensions.Config;

namespace UpKeepApi.Extensions;

public static class ConfigurationExtensions
{
    public static void ConfigurarWebAPI(this IServiceCollection services, IConfiguration Configuration)
    {
        JwtOptions JwtSetting = new JwtOptions(
            Configuration["JwtSettings:Issuer"],
            Configuration["JwtSettings:Audience"],
            Configuration["JwtSettings:Key"]
        );
        services.ConfigurarAuthentication(JwtSetting);
        services.AddConfigurationOptions(Configuration);
        services.ConfigurarLogger();
        services.ConfigurarAuthorization();
        services.RegisterMapsterConfiguration();
    }

    public static void AddConfigurationOptions(this IServiceCollection services, IConfiguration Configuration)
    {
        services.Configure<BucketConfig>(Configuration.GetSection("BucketConfig"));
        services.Configure<JwtOptions>(Configuration.GetSection("JwtSettings"));
    }
}