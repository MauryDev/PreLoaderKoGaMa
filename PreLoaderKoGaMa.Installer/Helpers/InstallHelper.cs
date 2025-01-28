
using System.IO.Compression;

namespace PreLoaderKoGaMa.Installer.Helpers
{
    internal class InstallHelper
    {
        public static void Install(string path, ZipArchive zipArchive)
        {
            foreach (ZipArchiveEntry zipArchiveEntry in zipArchive.Entries)
            {
                string destinationPath = Path.Combine(path, zipArchiveEntry.FullName);
                Directory.CreateDirectory(Path.GetDirectoryName(destinationPath)!);
                
                if (!string.IsNullOrEmpty(zipArchiveEntry.Name))
                {
                    zipArchiveEntry.ExtractToFile(destinationPath, overwrite: true);
                }
            }
        }
    }
}
