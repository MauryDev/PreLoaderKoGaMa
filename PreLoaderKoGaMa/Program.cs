using PreLoaderKoGaMa.Helpers;
using PreLoaderKoGaMa.Services;
using System.Diagnostics;
using System.Reflection;
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
            AppDomain.CurrentDomain.AssemblyResolve += (sender, eventArgs) =>
            {
                var name = new AssemblyName(eventArgs.Name).Name ;
                if (!name.EndsWith(".dll"))
                    name += ".dll";
                var pathdll = Directory.EnumerateFiles(PathHelp.PluginsPath, "*.dll", SearchOption.AllDirectories).Where((v) => Path.GetFileName(v) == name).FirstOrDefault();
                return pathdll == null ? null : Assembly.LoadFile(pathdll) ;
            };
            var args = Environment.GetCommandLineArgs();
            var argc = args.Length;

            switch (argc)
            {
                case 2:
                    await HandleTwoArgs(args[1]);
                    break;
                case 3:
                    await HandleThreeArgs(args[1], args[2]);
                    break;
               
                default:
                    if (argc > 3)
                    {
                        await Normal();
                    }
                    break;
            }
        }


        static async Task HandleTwoArgs(string arg)
        {
            switch (arg)
            {
                case "uninstall":
                    Uninstall();
                    break;
                case "install":
                    await Install();
                    break;
            }
        }

        static async Task HandleThreeArgs(string arg, string dllplugin)
        {
            switch (arg)
            {
                case "install-plugin":
                    InstallPlugin(dllplugin);
                    break;
                case "uninstall-plugin":
                    UninstallPlugin(dllplugin);
                    break;
            }
        }

        static void Uninstall()
        {
            PluginsHelper.UninstallExternalPlugins();
        }

        static async Task Install()
        {
            LogServiceStatus("Starting");
            var services = new ServiceManager();
            LogServiceStatus("Registering all services");

            RegisterBasicServices(services);
            RegisterAdvancedServices(services);

            LogServiceStatus("Building all services");
            await services.Build();
        }

        static void InstallPlugin(string plugins_args)
        {
            var plugins = plugins_args.Split(";");
            
            foreach (var plugin in plugins)
            {
                PluginsHelper.Install(plugin);

            }


        }

        static void UninstallPlugin(string plugins_args)
        {
            var plugins = plugins_args.Split(";");

            foreach (var plugin in plugins)
            {
                PluginsHelper.Uninstall(plugin);
            }
        }

        static async Task Normal()
        {
            if (ConfigService.SRead("PreLoaderKoGaMa", "Debug") == "false")
            {
                var handle = GetConsoleWindow();
                ShowWindow(handle, SW_HIDE);
            }

            LogServiceStatus("Starting");
            var services = new ServiceManager();
            LogServiceStatus("Registering all services");

            RegisterBasicServices(services);
            RegisterAdvancedServices(services);
            RegisterKoGaMaRun(services);
            LogServiceStatus("Building all services");
            await services.Build();
        }

        private static void LogServiceStatus(string status)
        {
            ConsoleHelper.Log("ServiceManager", status);
        }

        private static void RegisterBasicServices(ServiceManager serviceManager)
        {
            serviceManager.Register<ConfigService>();
            serviceManager.Register<ConsoleTools>();

        }
        private static void RegisterKoGaMaRun(ServiceManager serviceManager)
        {
            serviceManager.Register<KoGaMaRun>();

        }
        private static void RegisterAdvancedServices(ServiceManager serviceManager)
        {
            serviceManager.InitExternalPlugins();
        }
    }
}