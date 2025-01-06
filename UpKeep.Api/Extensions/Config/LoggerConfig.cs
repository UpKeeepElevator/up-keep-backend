using Serilog;

namespace UpKeepApi.Extensions.Config;

public static class LoggerConfig
{
    public static void ConfigurarLogger(this IServiceCollection services)
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console()
            .WriteTo.File("LOG/logfile.log", rollingInterval: RollingInterval.Day)
            .CreateLogger();
    }
}