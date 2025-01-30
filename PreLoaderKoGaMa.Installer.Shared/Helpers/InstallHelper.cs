using System.Diagnostics;
using System.IO.Compression;

namespace PreLoaderKoGaMa.Installer.Shared.Helpers;


internal class InstallHelper
{
    public static void Install(string launchPath, ZipArchive zipArchive)
    {
        foreach (ZipArchiveEntry zipArchiveEntry in zipArchive.Entries)
        {
            string destinationPath = Path.Combine(launchPath, zipArchiveEntry.FullName);
            Directory.CreateDirectory(Path.GetDirectoryName(destinationPath)!);
            if (string.IsNullOrEmpty(zipArchiveEntry.Name))
                continue;
            zipArchiveEntry.ExtractToFile(destinationPath, overwrite: true);
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
            process.WaitForExit();
        }
    }

    public static void Install(KoGaMaServer kogamaServer, ZipArchive zipArchive)
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
            Install(PathHelper.GetLauncher(path), zipArchive);
    }
}

