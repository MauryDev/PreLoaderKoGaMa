using PreLoaderKoGaMa.Helpers;
using PreLoaderKoGaMa.Services;
using System.Diagnostics;


namespace PreLoaderKoGaMa
{
    internal static class Program
    {
        static async Task Main()
        {
            LogServiceStatus("Starting");
            var services = new ServiceManager();
            LogServiceStatus("Registering all services");

            RegisterServices(services);
            
            LogServiceStatus("Building all services");

            await services.Build();
            Thread.Sleep(5000);
        }

        private static void LogServiceStatus(string status)
        {
            ConsoleHelper.Log("ServiceManager", status);
        }

        private static void RegisterServices(ServiceManager services)
        {
            services.Register<ConsoleTools>();
            services.Register<BepinexDownload>();
            services.Register<KoGaMaToolsDownload>();
            services.LoadExternalPlugins();
#if RELEASE
            services.Register<KoGaMaRun>();
#endif
        }
    }
}