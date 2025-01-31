

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

        public static void InitExternalPlugin(this ServiceManager serviceManager, string dllPath)
        {
            CreateAndInitPlugin(serviceManager, GetPluginType(dllPath));
        }

        public static void Uninstall(string dllPath)
        {
            CreateAndUninstallPlugin(GetPluginType(dllPath));
        }

        private static void CreateAndInitPlugin(ServiceManager serviceManager, Type? pluginType)
        {
            if (pluginType != null)
            {
                var plugin = (IPlugin)Activator.CreateInstance(pluginType);
                plugin.Init(serviceManager);
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
            var assembly = Assembly.LoadFrom(dllPath);
            return assembly.GetTypes().FirstOrDefault(x => typeof(IPlugin).IsAssignableFrom(x));
        }

        public static IEnumerable<Type?> GetAllPlugins()
        {
            var pathPlugin = PathHelp.PluginsPath;
            if (!Directory.Exists(pathPlugin))
            {
                Directory.CreateDirectory(pathPlugin);
            }

            return Directory.EnumerateFiles(pathPlugin, "*.plugin.dll", SearchOption.AllDirectories).Select(GetPluginType);
        }
    }
}
