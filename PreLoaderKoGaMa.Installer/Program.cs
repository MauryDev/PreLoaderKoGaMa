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
            var githubConfig = JsonSerializer.Deserialize<GithubRepositoryInfo>(Resources.githubconfig);
            if (githubConfig != null)
            {
                GithubRawHelper.Current = githubConfig;
            }
            else
            {
                throw new InvalidOperationException("Failed to deserialize GithubRepositoryInfo from resources.");
            }

            ApplicationConfiguration.Initialize();
            Application.Run(new FormMain());
        }
    }
}