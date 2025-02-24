

using PreLoaderKoGaMa.Services;
using System.Reflection;

namespace PreLoaderKoGaMa.Helpers
{
    internal static class PluginsHelper
    {
        public static void UninstallExternalPlugins()
        {
            foreach (var pluginType in GetAllPlugins())
            {
                CreateAndUninstallPlugin(pluginType);
            }
        }

        public static void InitExternalPlugins(this ServiceManager serviceManager)
        {
            foreach (var pluginType in GetAllPlugins())
            {
                CreateAndInitPlugin(serviceManager, pluginType);
            }
        }
        public static void InstallExternalPlugin(string dllPath)
        {
            CreateAndInstallPlugin(GetPluginType(dllPath));
        }
        public static void InitExternalPlugin(this ServiceManager serviceManager, string dllPath)
        {
            CreateAndInitPlugin(serviceManager, GetPluginType(dllPath));
        }

        public static void Uninstall(string dllPath)
        {
            CreateAndUninstallPlugin(GetPlugin(dllPath));
        }
        public static void Install(string dllPath)
        {
            CreateAndInstallPlugin(GetPlugin(dllPath));
        }

        private static void CreateAndInitPlugin(ServiceManager serviceManager, Type? pluginType)
        {
            if (pluginType != null)
            {
                var plugin = (IPlugin)Activator.CreateInstance(pluginType);
                plugin.Init(serviceManager);
            }
        }
        private static void CreateAndInstallPlugin( Type? pluginType)
        {
            if (pluginType != null)
            {
                var plugin = (IPlugin)Activator.CreateInstance(pluginType);
                plugin.Install();
            }
        }

        private static void CreateAndUninstallPlugin(Type? pluginType)
        {
            if (pluginType != null)
            {
                var plugin = (IPlugin)Activator.CreateInstance(pluginType);
                plugin.Uninstall();
            }
        }

        public static Type? GetPluginType(string dllPath)
        {
            var assembly = Assembly.LoadFile(dllPath);
            return assembly.GetTypes().FirstOrDefault(x => typeof(IPlugin).IsAssignableFrom(x));
        }

        public static IEnumerable<Type?> GetAllPlugins()
        {
            var pathPlugin = PathHelp.PluginsPath;
            if (!Directory.Exists(pathPlugin))
            {
                Directory.CreateDirectory(pathPlugin);
            }
            

            var pluginFiles = Directory.EnumerateDirectories(pathPlugin)
                                       .SelectMany(dir => Directory.EnumerateFiles(dir, "*.plugin.dll", SearchOption.TopDirectoryOnly)
                                                                    .Take(1));
            
            return pluginFiles.Select(GetPluginType);
        }
        public static Type? GetPlugin(string plugin_name)
        {
            var pathPlugins = PathHelp.PluginsPath;
            if (!Directory.Exists(pathPlugins))
            {
                Directory.CreateDirectory(pathPlugins);
                return null;
            }
            var pluginDirectory = Path.Combine(pathPlugins, plugin_name);
            if (!Directory.Exists(pluginDirectory))
            {
                return null;
            }
            var pluginFiles = Directory.EnumerateFiles(pluginDirectory, "*.plugin.dll", SearchOption.TopDirectoryOnly).Take(1);
            return pluginFiles.Select(GetPluginType).FirstOrDefault();
        }
    }
}
