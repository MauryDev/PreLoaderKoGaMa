namespace PreLoaderKoGaMa.Installer.Shared.Helpers;


internal class PathHelper
{
    public static string GetLocalAppDataPath => Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

    public static string WWWPath => Path.Combine(
        GetLocalAppDataPath,
        "KogamaLauncher-WWW"
        );
    public static string BRPath => Path.Combine(
        GetLocalAppDataPath,
        "KogamaLauncher-BR"
        );
    public static string FriendsPath => Path.Combine(
        GetLocalAppDataPath,
        "KogamaLauncher-Friends"
        );

    public static string GetLauncher(string basePath) => Path.Combine(
        basePath,
        "Launcher"
        );



    public static string GetConfigApp(string basePath) => Path.Combine(
        GetLauncher(basePath),
        "Config"
        );
    public static string GetPluginsApp(string basePath) => Path.Combine(
       GetLauncher(basePath),
       "Plugins"
       );
    public static string GetStandalone(string basePath) => Path.Combine(
        GetLauncher(basePath),
        "Standalone"
        );
}
