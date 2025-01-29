
namespace PreLoaderKoGaMa.Helpers
{
    internal class PathHelp
    {
        public static string LocalPath = AppContext.BaseDirectory;
        public static string KoGaMaStandalonePath => Path.Combine(LocalPath, "Standalone");
        public static string PluginsPath => Path.Combine(LocalPath, "Plugins");
        public static string ConfigPath => Path.Combine(LocalPath, "Config");


    }
}
