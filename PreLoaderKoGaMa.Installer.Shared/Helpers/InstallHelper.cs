using System.IO.Compression;

namespace PreLoaderKoGaMa.Installer.Shared.Helpers;


internal class InstallHelper
{
    public static void Install(string path, ZipArchive zipArchive)
    {
        foreach (ZipArchiveEntry zipArchiveEntry in zipArchive.Entries)
        {
            string destinationPath = Path.Combine(path, zipArchiveEntry.FullName);
            Directory.CreateDirectory(Path.GetDirectoryName(destinationPath)!);
            if (string.IsNullOrEmpty(zipArchiveEntry.Name))
                continue;
            zipArchiveEntry.ExtractToFile(destinationPath, overwrite: true);
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

