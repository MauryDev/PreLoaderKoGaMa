using PreLoaderKoGaMa.Helpers;
using PreLoaderKoGaMa.Services;
using System.Diagnostics;


namespace PreLoaderKoGaMa
{
    internal static class Program
    {

        static async Task Main()
        {
            ConsoleHelper.Log("ServiceManager", "Starting");
            var Services = new ServiceManager();
            ConsoleHelper.Log("ServiceManager", "Registering all services");

            Services.Register<ConsoleTools>();
            Services.Register<BepinexDownload>();
            Services.Register<KoGaMaToolsDownload>();
            Services.LoadExternalPlugins();
#if RELEASE
            Services.Register<KoGaMaRun>();
#endif
            ConsoleHelper.Log("ServiceManager", "Building all services");

            await Services.Build();

        }
    }
}