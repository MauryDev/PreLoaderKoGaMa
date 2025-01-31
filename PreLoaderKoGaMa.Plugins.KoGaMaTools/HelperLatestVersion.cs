using PreLoaderKoGaMa.Helpers;
using System.Text.RegularExpressions;

namespace PreLoaderKoGaMa.Plugins.KoGaMaTools
{
    public static class HelperLatestVersion
    {

        internal static GithubRepositoryInfo KoGaMaTools = new()
        {
            Author = "Beckowl",
            Repository = "KogamaTools",
            Branch = "master"
        };

        public static string? GetVersion(string rawText)
        {
            if (string.IsNullOrEmpty(rawText))
            {
                throw new ArgumentException("rawText não pode ser nulo ou vazio");
            }

            string pattern = @"ModVersion\s*=\s*""(.*?)"";";
            Match match = Regex.Match(rawText, pattern);
            if (match.Success)
            {
                return match.Groups[1].Value;
            }
            else
            {
                return null;
            }
        }

        public static async Task<string?> GetVersionByAPI(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentException("Parâmetros não podem ser nulos ou vazios");
            }

            using HttpClient client = new();
            try
            {
                string rawText = await client.GetStringAsync(GithubRawHelper.GetUrl(KoGaMaTools, path));
                var version = GetVersion(rawText);
                return version;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Erro ao obter a versão pela API", ex);
            }
        }
        static string GetURLLastRelease(string prefixfile, string version)
        {
            if (string.IsNullOrEmpty(version))
            {
                throw new ArgumentException("Parâmetros não podem ser nulos ou vazios");
            }
            return $"https://github.com/{KoGaMaTools.Author}/{KoGaMaTools.Repository}/releases/latest/download/{prefixfile}{version}.zip";
        }
        public static async Task<Stream> DownloadLastReleaseFile(string prefixfile, string version)
        {
            if (string.IsNullOrEmpty(version))
            {
                throw new ArgumentException("Parâmetros não podem ser nulos ou vazios");
            }
            using HttpClient client = new();
            return await client.GetStreamAsync(GetURLLastRelease(prefixfile, version));
        }

        public static async Task<Stream> GetLastReleaseStream()
        {

            var version = await GetVersionByAPI("src/ModInfo.cs");
            return await DownloadLastReleaseFile("KogamaTools.v", version);
        }


        public static bool CanInstallKoGaMaTools => !Directory.Exists(Path.Combine(PathHelp.LocalPath, "Standalone/BepInEx/Plugins/KoGaMaTools"));



    }
}
