

using PreLoaderKoGaMa.Services;
using System.Reflection;

namespace PreLoaderKoGaMa.Helpers
{
    internal static class PluginsService
    {
        public static void LoadExternalPlugins(this ServiceManager serviceManager)
        {
            var pathPlugin = PathHelp.PluginsPath;
            if (!Directory.Exists(pathPlugin))
            {
                Directory.CreateDirectory(pathPlugin);
            }
            Directory.EnumerateFiles(pathPlugin, "*.plugin.dll", SearchOption.AllDirectories).ToList().ForEach(file =>
            {
                var assembly = Assembly.LoadFrom(file);
                var MainPlugin = assembly
                .GetTypes()
                .Where(x => x.GetInterfaces().Contains(typeof(IPlugin)))
                .FirstOrDefault();

                if (MainPlugin != null)
                {
                    var plugin = (IPlugin)Activator.CreateInstance(MainPlugin);
                    plugin.Init(serviceManager);
                }

            });
        }
    }
}
