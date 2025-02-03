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
        async Task Install(Stream streamzip, string version)
        {
            var pluginPath = KoGaMaToolsInstallPath;
            var pluginversionPath = Path.Combine(pluginPath, "KogamaTools", "VERSION.txt");


            classLogger.Log("Loading KoGaMa Tools zip");

            using ZipArchive koGaMaToolsArchive = new(streamzip);
           
            classLogger.Log("Extracting KoGaMa Tools");

            foreach (ZipArchiveEntry zipArchiveEntry in koGaMaToolsArchive.Entries)
            {
                string destinationPath = Path.Combine(pluginPath, zipArchiveEntry.FullName);
                Directory.CreateDirectory(Path.GetDirectoryName(destinationPath)!);
                if (string.IsNullOrEmpty(zipArchiveEntry.Name))
                {
                    continue;
                }
                using Stream stream = zipArchiveEntry.Open();
                using FileStream fileStream = new(destinationPath, FileMode.Create);
                await stream.CopyToAsync(fileStream);
            }
            File.WriteAllText(pluginversionPath, version);

            classLogger.Log("Installed");

        }
        async Task<bool> TryUpdate(string version)
        {
            var pluginPath = KoGaMaToolsInstallPath;
            var pluginversionPath = Path.Combine(pluginPath, "KogamaTools", "VERSION.txt");
            if (File.Exists(pluginversionPath))
            {
                var getversion = File.ReadAllText(pluginversionPath) == version;
                if (getversion)
                {
                    return false;
                }
                
            }
            using var stream = await HelperLatestVersion.DownloadLastReleaseFile("KogamaTools.v", version);

            await Install(stream, version);
            return true;
        }
        public async Task RunAsync()
        {
            try
            {
                var version = await HelperLatestVersion.GetVersionByAPI("src/ModInfo.cs");
                classLogger.Log("Checking if it is installed");
                if (!HelperLatestVersion.CanInstallKoGaMaTools)
                {
                    if (!(await TryUpdate(version)))
                    {
                        classLogger.Log("All dependencies are installed");
                        return;
                    }
                    classLogger.Log("All dependencies are updated");

                }

                classLogger.Log("Downloading the latest release of KoGaMa Tools");
                using Stream releaseStream = await HelperLatestVersion.DownloadLastReleaseFile("KogamaTools.v", version);

                await Install(releaseStream,version);
            }
            catch (Exception err)
            {
                classLogger.Error("Error installing KoGaMa Tools: " + err);
            }
        }
    }
}
