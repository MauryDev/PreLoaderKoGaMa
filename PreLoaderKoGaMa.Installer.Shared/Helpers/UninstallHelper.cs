using System.IO.Compression;

namespace PreLoaderKoGaMa.Installer.Shared.Helpers;

internal class UninstallHelper
{
    public static void Uninstall(string LaunchPath, ZipArchive zipArchive)
    {
        foreach (ZipArchiveEntry zipArchiveEntry in zipArchive.Entries)
        {
            string destinationPath = Path.Combine(LaunchPath, zipArchiveEntry.FullName);

            if (string.IsNullOrEmpty(zipArchiveEntry.Name) || File.Exists(destinationPath))
                continue;
            File.Delete(destinationPath);
        }
        PatchDll.Patch(Path.Combine(LaunchPath, "LauncherCore.dll"));

        var pluginsPath = Path.Combine(LaunchPath, "Plugins");
        if (Directory.Exists(pluginsPath))
            Directory.Delete(pluginsPath, true);

        
    }
    public static void Uninstall(KoGaMaServer kogamaServer, ZipArchive zipArchive)
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
            Uninstall(path, zipArchive);
    }
}
