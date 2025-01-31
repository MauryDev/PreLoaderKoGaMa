using PreLoaderKoGaMa.Helpers;
using PreLoaderKoGaMa.Services;

using System.IO.Compression;


namespace PreLoaderKoGaMa.Plugins.KoGaMaTools
{
    public class KoGaMaToolsDownload
    {
        public ConsoleTools classLogger;
        public string KoGaMaToolsInstallPath;

        public void Init(ConsoleTools consoleTools, BepInEx.BepinexDownload bepinexDownload)
        {
            classLogger = consoleTools;
            KoGaMaToolsInstallPath = Path.Combine(bepinexDownload.BepinexPath, "Plugins");
        }
        public async Task RunAsync()
        {
            try
            {
                classLogger.Log("Checking if it is installed");
                if (!HelperLatestVersion.CanInstallKoGaMaTools)
                {
                    classLogger.Log("All dependencies are installed");
                    return;
                }
                var pluginPath = KoGaMaToolsInstallPath;

                classLogger.Log("Downloading the latest release of KoGaMa Tools");
                Stream releaseStream = await HelperLatestVersion.GetLastReleaseStream();

                classLogger.Log("Loading KoGaMa Tools zip");

                using ZipArchive koGaMaToolsArchive = new(releaseStream);

                classLogger.Log("Extracting KoGaMa Tools");

                foreach (ZipArchiveEntry zipArchiveEntry in koGaMaToolsArchive.Entries)
                {
                    string destinationPath = Path.Combine(pluginPath, zipArchiveEntry.FullName);
                    Directory.CreateDirectory(Path.GetDirectoryName(destinationPath)!);

                    if (!string.IsNullOrEmpty(zipArchiveEntry.Name))
                    {
                        zipArchiveEntry.ExtractToFile(destinationPath, overwrite: true);
                    }
                }
                classLogger.Log("Installed");
            }
            catch (Exception err)
            {
                classLogger.Error("Error installing KoGaMa Tools: " + err);
            }
        }
    }
}
