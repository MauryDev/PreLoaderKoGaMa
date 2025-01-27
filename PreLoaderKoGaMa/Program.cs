using PreLoaderKoGaMa.Helpers;
using System.Diagnostics;
using System.Reflection;


namespace PreLoaderKoGaMa
{
    internal static class Program
    {

        static async Task Main()
        {
            await HelperLatestVersion.Download();
#if RELEASE
            var kogamaPath = Path.Combine(PathHelp.LocalPath, "Standalone", "kogama.exe");

            var kogamainfo = new ProcessStartInfo(kogamaPath)
            {
                Arguments = string.Join(' ', Environment.GetCommandLineArgs())
            };
            ConsoleHelper.WriteMessage("KoGaMa", "Opening process");
            var kogamaexe = Process.Start(kogamainfo);
            Thread.Sleep(5000);
#endif
            
        }
    }
}