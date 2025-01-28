using PreLoaderKoGaMa.Helpers;
using System.IO.Compression;


namespace PreLoaderKoGaMa.Services
{
    public class BepinexDownload
    {
        public ConsoleTools.ClassPrint classLogger;
        public string BepinexInstallPath => PathHelp.KoGaMaStandalonePath;
        public string BepinexPath => Path.Combine(PathHelp.KoGaMaStandalonePath, "BepInEx");

        public void Init(ConsoleTools consoleTools)
        {
            classLogger = consoleTools.CreateClassLog<BepinexDownload>();
        }

        public async Task RunAsync()
        {
            try
            {
                classLogger.Log("Checking if Bepinex is installed");
                if (!HelperLatestVersion.CanInstallBepinex)
                {
                    classLogger.Log("All dependencies are installed");
                    return;
                }

                var localPath = BepinexInstallPath;
                classLogger.Log("Downloading Bepinex");
                Stream bepinexStream = await HelperLatestVersion.DownloadBepinexAsync();

                classLogger.Log("Loading Bepinex zip");
                using ZipArchive bepinexZipArchive = new(bepinexStream);
                classLogger.Log("Extracting Bepinex zip");

                foreach (ZipArchiveEntry zipArchiveEntry in bepinexZipArchive.Entries)
                {
                    string destinationPath = Path.Combine(localPath, zipArchiveEntry.FullName);
                    Directory.CreateDirectory(Path.GetDirectoryName(destinationPath)!);
                    if (!string.IsNullOrEmpty(zipArchiveEntry.Name))
                    {
                        zipArchiveEntry.ExtractToFile(destinationPath, overwrite: true);
                    }
                }

                classLogger.Log("Bepinex installed successfully");
            }
            catch (Exception err)
            {
                classLogger.Error("Error installing Bepinex: " + err);
            }
        }
    }
}
