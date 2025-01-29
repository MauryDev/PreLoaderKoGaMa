using PreLoaderKoGaMa.Helpers;


namespace PreLoaderKoGaMa.Services
{
    public class ConsoleTools
    {
        IServiceCurrent current;
        public void Init(IServiceCurrent serviceCurrent)
        {
            current = serviceCurrent;
        }
        string Title => current.CurrentServiceType?.Name ?? string.Empty;
        public void Log(string message) => ConsoleHelper.Log(Title, message);
        public void Warn(string message) => ConsoleHelper.Warn(Title, message);
        public void Error(string message) => ConsoleHelper.Error(Title, message);
    }
}
