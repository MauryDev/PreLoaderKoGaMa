using PreLoaderKoGaMa.Helpers;
using PreLoaderKoGaMa.Services;

namespace PreLoaderKoGaMa.Plugins.BepInEx;

public class MainPlugin : IPlugin
{
    void IPlugin.Init(ServiceManager serviceManager)
    {
        serviceManager.Register<BepinexDownload>();
    }
    void IPlugin.Install()
    {

    }

    void IPlugin.Uninstall()
    {
        var path = Path.Combine(PathHelp.PluginsPath, "BepInEx");
        if (Directory.Exists(path))
        {
            Directory.Delete(path, true);
        }
    }
}
