using PreLoaderKoGaMa.Helpers;
using PreLoaderKoGaMa.Plugins.BepInEx;
using PreLoaderKoGaMa.Services;

namespace PreLoaderKoGaMa.Plugins.KoGaMaTools;

public class MainPlugin : IPlugin
{
    public void Install()
    {
        
    }

    void IPlugin.Init(ServiceManager serviceManager)
    {
        serviceManager.Register<BepinexDownload>();
        serviceManager.Register<KoGaMaToolsDownload>();
    }

    void IPlugin.Uninstall()
    {
        var path = Path.Combine(PathHelp.KoGaMaStandalonePath, "BepInEx", "Plugins", "KoGaMaTools");
        if (Directory.Exists(path))
        {
            Directory.Delete(path, true);
        }
    }
}
