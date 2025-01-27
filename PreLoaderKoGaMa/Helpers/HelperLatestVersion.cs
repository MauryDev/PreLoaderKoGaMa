
using System.IO.Compression;
using System.Text.RegularExpressions;

namespace PreLoaderKoGaMa.Helpers
{
    public static class HelperLatestVersion
    {
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

        public static async Task<string?> GetVersionByAPI(string author, string repository, string branch, string path)
        {
            if (string.IsNullOrEmpty(author) || string.IsNullOrEmpty(repository) || string.IsNullOrEmpty(branch) || string.IsNullOrEmpty(path))
            {
                throw new ArgumentException("Parâmetros não podem ser nulos ou vazios");
            }

            using HttpClient client = new();
            try
            {
                string rawText = await client.GetStringAsync($"https://raw.githubusercontent.com/{author}/{repository}/refs/heads/{branch}/{path}");
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
        public static async Task<Stream> DownloadLastReleaseFile(string author, string repository, string prefixfile, string version)
        {
            if (string.IsNullOrEmpty(author) || string.IsNullOrEmpty(repository) || string.IsNullOrEmpty(version))
            {
                throw new ArgumentException("Parâmetros não podem ser nulos ou vazios");
            }
            using HttpClient client = new();
            return await client.GetStreamAsync(GetURLLastRelease(author, repository, prefixfile, version));
        }
        public static async Task<Stream> DownloadBepinexAsync()
        {
            using HttpClient client = new();
            string url = "https://cdn.discordapp.com/attachments/1304111711497752636/1333398230582431855/be.692851521c.zip?ex=6798bf5f&is=67976ddf&hm=b3c1b230a468c2b539399de7b9aa3db50500c8ef37038e6622b5150cf90dc243&";
            HttpResponseMessage response = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();
            Stream stream = await response.Content.ReadAsStreamAsync();
            return stream;
        }
        public static async Task<Stream> GetLastReleaseStream()
        {
            string author = "Beckowl",
                repository = "KogamaTools";
            var version = await GetVersionByAPI(author, repository, "master", "src/ModInfo.cs");
            var downloadstream = await DownloadLastReleaseFile(author, repository, "KogamaTools.v", version);
            return downloadstream;
        }
        public static bool CanInstall
        {
            get
            {
                return !Directory.Exists(Path.Combine(PathHelp.LocalPath, "Standalone/BepInEx"));
            }
        }
        public static async Task Download()
        {
            ConsoleHelper.WriteMessage("Download", "Checking if is installed all dependencies");

            if (!CanInstall)
            {
                ConsoleHelper.WriteMessage("Download", "All dependencies is installed");
                return;
            }
            var localPath = PathHelp.LocalPath;

            var installPath = Path.Combine(localPath, "Standalone");
            var pluginPath = Path.Combine(installPath, "BepInEx", "Plugins");
            ConsoleHelper.WriteMessage("Download", "Dowloading Bepinex");
            Stream stream = await DownloadBepinexAsync();
            ConsoleHelper.WriteMessage("Download", "Dowloading Last Release from KoGaMa Tools");
            Stream stream2 = await GetLastReleaseStream();
            ConsoleHelper.WriteMessage("Download", "Load zip Bepinex");

            using ZipArchive zipArchive = new(stream);
            ConsoleHelper.WriteMessage("Download", "Load zip KoGaMa Tools");

            using ZipArchive zipArchive2 = new(stream2);
            ConsoleHelper.WriteMessage("Download", "Extract Bepinex");

            foreach (ZipArchiveEntry zipArchiveEntry in zipArchive.Entries)
            {
                string destinationPath = Path.Combine(installPath, zipArchiveEntry.FullName);
                Directory.CreateDirectory(Path.GetDirectoryName(destinationPath)!);
                if (!string.IsNullOrEmpty(zipArchiveEntry.Name))
                {
                    zipArchiveEntry.ExtractToFile(destinationPath, overwrite: true);
                }
            }
            ConsoleHelper.WriteMessage("Download", "Extract KoGaMa Tools");

            foreach (ZipArchiveEntry zipArchiveEntry in zipArchive2.Entries)
            {
                string destinationPath = Path.Combine(pluginPath, zipArchiveEntry.FullName);
                Directory.CreateDirectory(Path.GetDirectoryName(destinationPath)!);

                if (!string.IsNullOrEmpty(zipArchiveEntry.Name))
                {
                    zipArchiveEntry.ExtractToFile(destinationPath, overwrite: true);
                }
            }
            ConsoleHelper.WriteMessage("Download", "Installed");

        }
    }
}
