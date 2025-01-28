using PreLoaderKoGaMa.Helpers;
using System.Diagnostics;


namespace PreLoaderKoGaMa.Services
{
    internal class KoGaMaRun
    {

        public ConsoleTools.ClassPrint classLogger;
        public void Init(ConsoleTools consoleTools)
        {
            classLogger = consoleTools.CreateClassLog<KoGaMaRun>();
        }
        public void Run()
        {
            var kogamaPath = Path.Combine(PathHelp.KoGaMaStandalonePath, "kogama.exe");

            var kogamainfo = new ProcessStartInfo(kogamaPath)
            {
                Arguments = string.Join(' ', Environment.GetCommandLineArgs())
            };
            classLogger.Log("Opening process");
            Process.Start(kogamainfo);
            Thread.Sleep(5000);
        }
    }
}
