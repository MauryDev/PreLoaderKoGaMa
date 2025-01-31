using PreLoaderKoGaMa.Plugins.BepInEx;
using PreLoaderKoGaMa.Services;

namespace PreLoaderKoGaMa.Plugins.KoGaMaTools;

public class MainPlugin : IPlugin
{
    void IPlugin.Init(ServiceManager serviceManager)
    {
        serviceManager.Register<BepinexDownload>();
        serviceManager.Register<KoGaMaToolsDownload>();
    }

    void IPlugin.Uninstall()
    {
        
    }
}
