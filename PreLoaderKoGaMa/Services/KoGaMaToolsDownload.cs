using PreLoaderKoGaMa.Helpers;
using System.IO.Compression;
namespace PreLoaderKoGaMa.Services
{
    public class KoGaMaToolsDownload
    {
        public ConsoleTools.ClassPrint classLogger;
        public string KoGaMaToolsInstallPath;

        public void Init(ConsoleTools consoleTools, BepinexDownload bepinexDownload)
        {
            classLogger = consoleTools.CreateClassLog<KoGaMaToolsDownload>();
            KoGaMaToolsInstallPath = Path.Combine(bepinexDownload.BepinexPath, "Plugins");
        }
        public async Task RunAsync()
        {
            try
            {
                classLogger.Log("Checking if is installed");
                if (!HelperLatestVersion.CanInstallKoGaMaTools)
                {
                    classLogger.Log("All dependencies is installed");
                    return;
                }
                var pluginPath = KoGaMaToolsInstallPath;

                classLogger.Log("Dowloading Last Release from KoGaMa Tools");
                Stream releaseStream = await HelperLatestVersion.GetLastReleaseStream();

                classLogger.Log("Load zip KoGaMa Tools");

                using ZipArchive koGaMaToolsArchive = new(releaseStream);

                classLogger.Log("Extract KoGaMa Tools");

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
            catch (Exception)
            {
                classLogger.Error("Error on install KoGaMa Tools");
            }
        }
    }
}
