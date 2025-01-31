using PreLoaderKoGaMa.Helpers;
using PreLoaderKoGaMa.Services;
using System.IO.Compression;

namespace PreLoaderKoGaMa.Plugins.BepInEx
{
    public class BepinexDownload
    {
        public static bool CanInstallBepinex => !Directory.Exists(Path.Combine(PathHelp.LocalPath, "Standalone/BepInEx"));

        internal static GithubRepositoryInfo PreLoaderKoGaMa = new()
        {
            Author = "MauryDev",
            Repository = "PreLoaderKoGaMa",
            Branch = "master"
        };

        public ConsoleTools classLogger;
        public string BepinexInstallPath => PathHelp.KoGaMaStandalonePath;
        public string BepinexPath => Path.Combine(PathHelp.KoGaMaStandalonePath, "BepInEx");

        public void Init(ConsoleTools consoleTools)
        {
            classLogger = consoleTools;
        }

        public async Task RunAsync()
        {
            try
            {
                classLogger.Log("Checking if Bepinex is installed");
                if (!CanInstallBepinex)
                {
                    classLogger.Log("All dependencies are installed");
                    return;
                }

                var localPath = BepinexInstallPath;
                classLogger.Log("Downloading Bepinex");
                Stream bepinexStream = await DownloadBepinexAsync();

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

        static async Task<Stream> DownloadBepinexAsync()
        {

            return await GithubRawHelper.GetStream(PreLoaderKoGaMa, "PreLoaderKoGaMa/src/be.692+851521c.zip");
        }
    }
}
