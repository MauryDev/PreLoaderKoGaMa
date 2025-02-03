using System.Diagnostics;
using System.IO;
using System.IO.Compression;

namespace PreLoaderKoGaMa.Installer.Shared.Helpers;


internal class InstallHelper
{
    public static async Task Install(string launchPath, ZipArchive zipArchive)
    {
        foreach (ZipArchiveEntry zipArchiveEntry in zipArchive.Entries)
        {
            string destinationPath = Path.Combine(launchPath, zipArchiveEntry.FullName);
            Directory.CreateDirectory(Path.GetDirectoryName(destinationPath)!);
            if (string.IsNullOrEmpty(zipArchiveEntry.Name))
                continue;
            using var entrystream = zipArchiveEntry.Open();
            using FileStream fileStream = new(destinationPath, FileMode.Create);
            await entrystream.CopyToAsync(fileStream);
        }
        PatchDll.Patch(Path.Combine(launchPath, "LauncherCore.dll"));

        Directory.CreateDirectory(Path.Combine(launchPath, "Config"));
        Directory.CreateDirectory(Path.Combine(launchPath, "Plugins"));

        var path = Path.Combine(launchPath, "PreLoaderKoGaMa.exe");
        if (File.Exists(path))
        {
            var process = new Process()
            {
                StartInfo = new ProcessStartInfo(path)
                {
                    Arguments = "install",
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };
            process.Start();
            await process.WaitForExitAsync();
        }
    }
    public static async Task InstallPlugin(string launchPath, ZipArchive zipArchive)
    {
        var pluginspath = Path.Combine(launchPath, "Plugins");
        if (!Directory.Exists(Path.Combine(launchPath, "Plugins")))
            return;

        foreach (ZipArchiveEntry zipArchiveEntry in zipArchive.Entries)
        {
            string destinationPath = Path.Combine(pluginspath, zipArchiveEntry.FullName);
            Directory.CreateDirectory(Path.GetDirectoryName(destinationPath)!);
            if (string.IsNullOrEmpty(zipArchiveEntry.Name))
                continue;
            using var entrystream = zipArchiveEntry.Open();
            using FileStream fileStream = new(destinationPath, FileMode.Create);
            await entrystream.CopyToAsync(fileStream);
        }
    }
    public static async Task Install(KoGaMaServer kogamaServer, ZipArchive zipArchive)
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
            await Install(PathHelper.GetLauncher(path), zipArchive);
    }
}

