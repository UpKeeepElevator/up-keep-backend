using System.Reflection;
using Mapster;

namespace UpKeepApi.Extensions.Config;

public static class MapsterConfig
{
    public static void RegisterMapsterConfiguration(this IServiceCollection services)
    {
        // TypeAdapterConfig<UsuarioDTO, VisitadoresView>
        //     .NewConfig()
        //     .Map(dest => dest.Visitador, src => src.nombres)
        //     .Map(dest => dest.cod_visitador, src => src.cod_usuario);


        TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());
    }
}