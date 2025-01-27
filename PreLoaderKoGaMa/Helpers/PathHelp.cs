using System.Reflection;


namespace PreLoaderKoGaMa.Helpers
{
    internal class PathHelp
    {
        public static string LocalPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? "";

    }
}
