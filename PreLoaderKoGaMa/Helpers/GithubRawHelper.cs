

namespace PreLoaderKoGaMa.Helpers
{
    internal class GithubRawHelper
    {
        internal class GithubRepositoryInfo
        {
            public string Author { get; set; } = string.Empty;
            public string Repository { get; set; } = string.Empty;
            public string Branch { get; set; } = string.Empty;
        }
        public static GithubRepositoryInfo Current { get; set; } = new();
        public static string GetUrl(GithubRepositoryInfo github, string path)
        {
            return GetUrl(github.Author, github.Repository, github.Branch, path);
        }
        public static string GetUrl(string author, string repository, string branch, string path)
        {
            return $"https://raw.githubusercontent.com/{author}/{repository}/refs/heads/{branch}/{path}";
        }
        public static string GetUrlCurrent(string path)
        {
            return GetUrl(Current, path);
        }
    }
}
