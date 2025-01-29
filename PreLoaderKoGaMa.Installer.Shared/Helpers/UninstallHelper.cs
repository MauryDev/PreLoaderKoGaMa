using System.IO.Compression;

namespace PreLoaderKoGaMa.Installer.Shared.Helpers;

internal class UninstallHelper
{
    public static void Uninstall(string path, ZipArchive zipArchive)
    {
        foreach (ZipArchiveEntry zipArchiveEntry in zipArchive.Entries)
        {
            string destinationPath = Path.Combine(path, zipArchiveEntry.FullName);

            if (string.IsNullOrEmpty(zipArchiveEntry.Name) || File.Exists(destinationPath))
                continue;
            File.Delete(destinationPath);
        }

        var pluginsPath = Path.Combine(path, "Plugins");
        if (Directory.Exists(pluginsPath))
            Directory.Delete(pluginsPath, true);

        var LauncherCore_Boostrap = Path.Combine(path, "../LauncherCore.dll");
        if (File.Exists(LauncherCore_Boostrap))
            File.Copy(LauncherCore_Boostrap, Path.Combine(path, "LauncherCore.dll"));
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
