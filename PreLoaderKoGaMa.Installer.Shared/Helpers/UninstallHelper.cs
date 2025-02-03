using System.Diagnostics;
using System.IO.Compression;

namespace PreLoaderKoGaMa.Installer.Shared.Helpers;

internal class UninstallHelper
{
    public static async Task Uninstall(string LaunchPath, ZipArchive zipArchive)
    {
       
        var path = Path.Combine(LaunchPath, "PreLoaderKoGaMa.exe");
        if (File.Exists(path))
        {
            var process = new Process()
            {
                StartInfo = new ProcessStartInfo(path)
                {
                    Arguments = "uninstall",
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };
            process.Start();
            await process.WaitForExitAsync();
        }
       

        foreach (ZipArchiveEntry zipArchiveEntry in zipArchive.Entries)
        {
            string destinationPath = Path.Combine(LaunchPath, zipArchiveEntry.FullName);

            if (string.IsNullOrEmpty(zipArchiveEntry.Name) || !File.Exists(destinationPath))
                continue;
            File.Delete(destinationPath);
        }

        PatchDll.Unpatch(Path.Combine(LaunchPath, "LauncherCore.dll"));

        var pluginsPath = Path.Combine(LaunchPath, "Plugins");
        if (Directory.Exists(pluginsPath))
            Directory.Delete(pluginsPath, true);

        var configPath = Path.Combine(LaunchPath, "Config");

        if (Directory.Exists(configPath))
            Directory.Delete(configPath, true);


    }
    public static async Task Uninstall(KoGaMaServer kogamaServer, ZipArchive zipArchive)
    {
        string path = string.Empty;
        switch (kogamaServer)
        {
            case KoGaMaServer.BR:
                path = PathHelper.BRPath;
                break;
            case KoGaMaServer.WWW:
                path = PathHelper.WWWPath;
                break;
            case KoGaMaServer.Friends:
                path = PathHelper.FriendsPath;
                break;
        }
        if (Directory.Exists(path))
            await Uninstall(PathHelper.GetLauncher(path), zipArchive);
    }
}
