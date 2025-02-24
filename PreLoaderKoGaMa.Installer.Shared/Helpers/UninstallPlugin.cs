using System.Diagnostics;

namespace PreLoaderKoGaMa.Installer.Shared.Helpers
{
    internal static class UninstallPlugin
    {

        public static async Task Uninstall(string[] launchpaths, string name)
        {
            foreach (var launchpath in launchpaths)
            {
                var pluginPath = Path.Combine(launchpath, "Plugins", name);
                if (Directory.Exists(pluginPath))
                {
                    ProcessStartInfo processStartInfo = new(Path.Combine(launchpath, "PreLoaderKoGaMa.exe"))
                    {
                        UseShellExecute = false,
                    };
                    processStartInfo.ArgumentList.AddRange(new string[] { "uninstall-plugin", name });

                    var process = new Process()
                    {
                        StartInfo = processStartInfo
                    };
                    process.Start();
                    await process.WaitForExitAsync();
                    Directory.Delete(pluginPath, true);
                }
            }
        }
    }
}
