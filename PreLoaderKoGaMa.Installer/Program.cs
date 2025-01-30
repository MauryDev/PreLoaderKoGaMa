using PreLoaderKoGaMa.Installer.Shared.Helpers;
using PreLoaderKoGaMa.Installer.Properties;
using System.Text.Json;

namespace PreLoaderKoGaMa.Installer
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            
            GithubRawHelper.Current = JsonSerializer.Deserialize<GithubRepositoryInfo>(Resources.githubconfig);
            

            ApplicationConfiguration.Initialize();
            Application.Run(new FormMain());
        }
    }
}