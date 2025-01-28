using System.IO.Compression;

namespace PreLoaderKoGaMa.Installer.Helpers
{
    internal class UninstallHelper
    {
        public static void Uninstall(string path, ZipArchive zipArchive)
        {
            foreach (ZipArchiveEntry zipArchiveEntry in zipArchive.Entries)
            {
                string destinationPath = Path.Combine(path, zipArchiveEntry.FullName);
                
                if (!string.IsNullOrEmpty(zipArchiveEntry.Name))
                {
                    if (File.Exists(destinationPath))
                    {
                        File.Delete(destinationPath);
                    }
                }
            }
            var pluginsPath = Path.Combine(path, "Plugins");
            if (Directory.Exists(pluginsPath))
            {
                Directory.Delete(pluginsPath, true);
            }
            var LauncherCore_Boostrap = Path.Combine(path, "../LauncherCore.dll");
            if (File.Exists(LauncherCore_Boostrap))
            {
                File.Copy(LauncherCore_Boostrap, Path.Combine(path,"LauncherCore.dll"));
            }
        }
    }
}
