using PreLoaderKoGaMa.Services;

namespace PreLoaderKoGaMa.Plugins.BepInEx;

public class MainPlugin : IPlugin
{
    void IPlugin.Init(ServiceManager serviceManager)
    {
        serviceManager.Register<BepinexDownload>();
    }

    void IPlugin.Uninstall()
    {

    }
}
