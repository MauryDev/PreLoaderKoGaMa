using PreLoaderKoGaMa.Helpers;
using PreLoaderKoGaMa.Services;
using System.Diagnostics;
using System.Runtime.InteropServices;


namespace PreLoaderKoGaMa
{
    internal static class Program
    {
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        const int SW_HIDE = 0;
        const int SW_SHOW = 5;
        static async Task Main()
        {
            if (ConfigService.SRead("PreLoaderKoGaMa","Debug") == "false")
            {
                var handle = GetConsoleWindow();
                ShowWindow(handle, SW_HIDE);
            }
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