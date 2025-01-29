

namespace PreLoaderKoGaMa.Services.Tools
{
    internal class ServiceCurrent : IServiceCurrent
    {
        object? currentService;
        object? IServiceCurrent.CurrentService => currentService;

        Type? IServiceCurrent.CurrentServiceType => currentService?.GetType();

        internal void SetService(object? service)
        {
            currentService = service;
        }

        T? IServiceCurrent.GetCurrentService<T>() where T : default
        {
            return currentService is T service ? service : default;
        }
    }
}
