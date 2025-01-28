
using System.IO.Compression;
using System.Text.RegularExpressions;

namespace PreLoaderKoGaMa.Helpers
{
    public static class HelperLatestVersion
    {
        internal static GithubRawHelper.GithubRepositoryInfo PreLoaderKoGaMa = new() {
            Author = "MauryDev",
            Repository = "PreLoaderKoGaMa",
            Branch = "master"
        };
        internal static GithubRawHelper.GithubRepositoryInfo KoGaMaTools = new()
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
        static string GetURLLastRelease(string author, string repository, string prefixfile, string version)
        {
            if (string.IsNullOrEmpty(author) || string.IsNullOrEmpty(repository) || string.IsNullOrEmpty(version))
            {
                throw new ArgumentException("Parâmetros não podem ser nulos ou vazios");
            }
            return $"https://github.com/{author}/{repository}/releases/latest/download/{prefixfile}{version}.zip";
        }
        public static async Task<Stream> DownloadLastReleaseFile(string prefixfile, string version)
        {
            if (string.IsNullOrEmpty(version))
            {
                throw new ArgumentException("Parâmetros não podem ser nulos ou vazios");
            }
            using HttpClient client = new();
            return await client.GetStreamAsync(GetURLLastRelease(KoGaMaTools.Author, KoGaMaTools.Repository, prefixfile, version));
        }
        public static async Task<Stream> DownloadBepinexAsync()
        {
            using HttpClient client = new();
            var url = GithubRawHelper.GetUrl(PreLoaderKoGaMa, "PreloaderKoGaMa/src/be.692+851521c.zip");
            HttpResponseMessage response = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();
            Stream stream = await response.Content.ReadAsStreamAsync();
            return stream;
        }
        public static async Task<Stream> GetLastReleaseStream()
        {
           
            var version = await GetVersionByAPI("src/ModInfo.cs");
            var downloadstream = await DownloadLastReleaseFile("KogamaTools.v", version);
            return downloadstream;
        }
        public static bool CanInstallBepinex => !Directory.Exists(Path.Combine(PathHelp.LocalPath, "Standalone/BepInEx"));
            
        
        public static bool CanInstallKoGaMaTools => !Directory.Exists(Path.Combine(PathHelp.LocalPath, "Standalone/BepInEx/Plugins/KoGaMaTools"));
        
   
        
    }
}
