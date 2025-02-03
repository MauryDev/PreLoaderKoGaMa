
namespace PreLoaderKoGaMa.Services
{
    public interface IPlugin
    {
        void Init(ServiceManager serviceManager);
        void Install();
        void Uninstall();
    }
}
