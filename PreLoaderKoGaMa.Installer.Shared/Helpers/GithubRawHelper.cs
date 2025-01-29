

namespace PreLoaderKoGaMa.Installer.Shared.Helpers;

internal class GithubRawHelper
{
    
    public static GithubRepositoryInfo Current { get; set; } = new();
    public static async Task<Stream> GetStream(GithubRepositoryInfo github, string path)
    {
        return await GetStream(github.Author, github.Repository, github.Branch, path);
    }
    public static async Task<Stream> GetStream(string author, string repository, string branch, string path)
    {
        using HttpClient client = new();
        return await client.GetStreamAsync(GetUrl(author, repository, branch, path));
    }
    public static async Task<Stream> GetStreamCurrent(string path)
    {
        using HttpClient client = new();
        return await GetStream(Current,path);
    }
    public static string GetUrl(GithubRepositoryInfo github, string path)
    {
        return GetUrl(github.Author, github.Repository, github.Branch, path);
    }
    public static string GetUrl(string author, string repository, string branch, string path)
    {

        return $"https://github.com/{author}/{repository}/raw/refs/heads/{branch}/{path}";
    }
    public static string GetUrlCurrent(string path)
    {
        return GetUrl(Current, path);
    }
}
