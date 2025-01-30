

using PreLoaderKoGaMa.Services;
using System.Reflection;

namespace PreLoaderKoGaMa.Helpers
{
    internal static class PluginsHelper
    {
        public static void UninstallExternalPlugins()
        {
            var plugins = GetAllPlugins();
            foreach (var mainplugin in plugins)
            {
                if (mainplugin != null)
                {
                    var plugin = (IPlugin)Activator.CreateInstance(mainplugin);
                    plugin.Uninstall();
                }
            }
            
        }
        public static void InitExternalPlugins(this ServiceManager serviceManager)
        {
            var plugins = GetAllPlugins();
            foreach (var mainplugin in plugins)
            {
                if (mainplugin != null)
                {
                    var plugin = (IPlugin)Activator.CreateInstance(mainplugin);
                    plugin.Init(serviceManager);
                }
            }

        }
        public static IEnumerable<Type?> GetAllPlugins()
        {
            var pathPlugin = PathHelp.PluginsPath;
            if (!Directory.Exists(pathPlugin))
            {
                Directory.CreateDirectory(pathPlugin);
            }
            
            return Directory.EnumerateFiles(pathPlugin, "*.plugin.dll", SearchOption.AllDirectories).Select((dll) =>
            {
                var assembly = Assembly.LoadFrom(dll);
                var MainPlugin = assembly
                .GetTypes()
                .Where(x => x.GetInterfaces().Contains(typeof(IPlugin)))
                .FirstOrDefault();
                return MainPlugin;
            });
        }

    }
}
