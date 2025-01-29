using PreLoaderKoGaMa.Helpers;
using System.Diagnostics;


namespace PreLoaderKoGaMa.Services
{
    internal class KoGaMaRun
    {

        public ConsoleTools consoleTools;
        public void Init(ConsoleTools consoleTools)
        {
            this.consoleTools = consoleTools;
        }
        public void Run()
        {
            var kogamaPath = Path.Combine(PathHelp.KoGaMaStandalonePath, "kogama.exe");

            var kogamainfo = new ProcessStartInfo(kogamaPath)
            {
                Arguments = string.Join(' ', Environment.GetCommandLineArgs())
            };
            consoleTools.Log("Opening process");
            Process.Start(kogamainfo);
            Thread.Sleep(5000);
        }
    }
}
