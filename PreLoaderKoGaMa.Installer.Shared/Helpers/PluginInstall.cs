
using System.Diagnostics;
using System.IO.Compression;
using System.Text.Json;

namespace PreLoaderKoGaMa.Installer.Shared.Helpers;

public static class PluginInstall
{

    public static async Task ExtractFiles(string packagepath, ZipArchive zipArchive )
    {
        foreach (ZipArchiveEntry zipArchiveEntry in zipArchive.Entries)
        {

            if (string.IsNullOrEmpty(zipArchiveEntry.Name) || zipArchiveEntry.FullName == "package.json")
                continue;

            string destinationPath = Path.Combine(packagepath, zipArchiveEntry.FullName);
            Directory.CreateDirectory(Path.GetDirectoryName(destinationPath)!);
            using var entrystream = zipArchiveEntry.Open();
            using FileStream fileStream = new(destinationPath, FileMode.Create);
            await entrystream.CopyToAsync(fileStream);
        }
    }
    public static async Task Install(string[] launchpaths, ZipArchive zipArchive)
    {
        var entrypackage = zipArchive.GetEntry("package.json");
        if (entrypackage == null)
            return;
        var package = JsonSerializer.Deserialize<PackageInfo>(entrypackage.Open());
        var name = package.Name;

       

        foreach (var launchpath in launchpaths)
        {

            var packagepath = Path.Combine(launchpath, "Plugins", name);
            if (Directory.Exists(packagepath))
                continue;

            await ExtractFiles(packagepath, zipArchive);
        }
        foreach (var dependencyInfo in package.Dependencies)
        {
            if (dependencyInfo.Type == "file")
            {
                await PluginHelper.OnInstallFile(launchpaths, dependencyInfo.Value);
            } else if (dependencyInfo.Type == "url")
            {
                await PluginHelper.OnInstallurl(launchpaths, dependencyInfo.Value);
            }
            else if (dependencyInfo.Type == "official")
            {
                await PluginHelper.OnInstallOficial(launchpaths, dependencyInfo.Value);
            }
        }
        foreach (var launchpath in launchpaths)
        {
            var packagepath = Path.Combine(launchpath, "Plugins", name);
            if (Directory.Exists(packagepath))
                continue;
            ProcessStartInfo processStartInfo = new(Path.Combine(launchpath, "PreLoaderKoGaMa.exe"))
            {
                UseShellExecute = false,
                CreateNoWindow = true
            };
            processStartInfo.ArgumentList.AddRange(new string[] { "install-plugin", name });
            
            var process = new Process()
            {
                StartInfo = processStartInfo
            };
            process.Start();
            await process.WaitForExitAsync();
        }
    }
}
