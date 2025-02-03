using System.IO.Compression;
using System.Text.Json;

namespace PreLoaderKoGaMa.Installer.Shared.Helpers;

public static class PluginHelper
{
    public static async Task OnInstallOficial(string[] launchpauth,string PluginName)
    {
        using var stream = await GithubRawHelper.GetStreamCurrent("Packages/packages-info.json");
        var packages = JsonSerializer.Deserialize<Dictionary<string, string>>(stream);
        foreach (var package in packages)
        {
            if (package.Key == PluginName)
            {
                using var client = new HttpClient();
                using var pluginStream = await client.GetStreamAsync(package.Value);
                using var zip = new ZipArchive(pluginStream);

                await PluginInstall.Install(launchpauth, zip);
                break;
            }
        }
    }
    public static async Task OnInstallurl(string[] launchpaths, string url)
    {
        using var client = new HttpClient();
        using var pluginStream = await client.GetStreamAsync(url);
        using var zip = new ZipArchive(pluginStream);

        await PluginInstall.Install(launchpaths, zip);
    }
    public static async Task OnInstallFile(string[] launchpaths, string pathfile)
    {
        using var Fs = new FileStream(pathfile, FileMode.Open,FileAccess.Read);
        using var zip = new ZipArchive(Fs);

        await PluginInstall.Install(launchpaths, zip);
    }
}
