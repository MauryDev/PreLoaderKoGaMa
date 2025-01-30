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
            var args = Environment.GetCommandLineArgs();
            var argc = args.Length;
            if (argc == 2)
            {
                var arg = args[1];
                if (arg == "uninstall")
                {
                    PluginsHelper.UninstallExternalPlugins();

                } else if (arg == "install")
                {
                    await RunKoGaMa(false);
                    Thread.Sleep(5000);
                }
            }
            else
            {
                await RunKoGaMa(true);
                Thread.Sleep(5000);

            }


        }
        async static Task RunKoGaMa(bool openKoGaMa)
        {
            if (ConfigService.SRead("PreLoaderKoGaMa", "Debug") == "false")
            {
                var handle = GetConsoleWindow();
                ShowWindow(handle, SW_HIDE);
            }
            LogServiceStatus("Starting");
            var services = new ServiceManager();
            LogServiceStatus("Registering all services");

            RegisterServices(services, openKoGaMa);

            LogServiceStatus("Building all services");

            await services.Build();
        }
        private static void LogServiceStatus(string status)
        {
            ConsoleHelper.Log("ServiceManager", status);
        }

        private static void RegisterServices(ServiceManager services, bool openKoGaMa)
        {
            services.Register<ConsoleTools>();
            services.Register<BepinexDownload>();
            services.Register<KoGaMaToolsDownload>();
            services.InitExternalPlugins();
#if RELEASE
            if (openKoGaMa)
            {
                services.Register<KoGaMaRun>();
            }
#endif
        }
    }
}