using Microsoft.EntityFrameworkCore;
using UpKeep.Data;
using UpKeep.Data.Context;
using UpKeep.Data.Contracts;
using UpKeepApi.Extensions.Config;
using UpKepp.Services;
using UpKepp.Services.Contracts;

namespace UpKeepApi.Extensions;

public static class ServicesExtension
{
    public static void ConfigurarServicios(this IServiceCollection Services, ConfigurationManager _configuration)
    {
        Services.AddFluentEmail(_configuration);
        Services.AddControllers();
        Services.AddEndpointsApiExplorer();
        Services.AddDbContext<UpKeepDbContext>(options =>
            options.UseNpgsql(_configuration.GetConnectionString("upKeep") ?? ""));

        Services.AddScoped<IRepositorioManager, RepositorioManager>();
        Services.AddScoped<IServicioManager, ServicioManager>();
    }
}