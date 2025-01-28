using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreLoaderKoGaMa.Installer.Helpers
{
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

        public static string GetLauncher(string basePath)
        {
            return Path.Combine(
                basePath,
                "Launcher"
                );
        }
    }
}
