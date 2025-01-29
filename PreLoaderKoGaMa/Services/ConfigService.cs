
using PreLoaderKoGaMa.Helpers;
using System.Text.Json;

namespace PreLoaderKoGaMa.Services
{
    using ConfigJSON = Dictionary<string, Dictionary<string, string>>;

    internal class ConfigService
    {
        
        public static string ConfigFile => Path.Combine(PathHelp.PluginsPath, "config.json");

        IServiceCurrent current;
        public void Init(IServiceCurrent serviceCurrent)
        {
            current = serviceCurrent;
        }
        string SectionName => current.CurrentServiceType?.Name ?? string.Empty;

        static void CreateIfNotExists()
        {
            if (!File.Exists(ConfigFile))
            {
                File.WriteAllText(ConfigFile, "{}");
            }
        }
        public static string? SRead(string section, string key)
        {
            CreateIfNotExists();
            var data = JsonSerializer.Deserialize<ConfigJSON>(File.ReadAllText(ConfigFile));


            if (data.TryGetValue(section, out var sectionData))
            {
                if (sectionData.TryGetValue(key, out var value))
                {
                    return value;
                }
            }
            return null;
        }
        public static void SWrite(string section, string key, string value)
        {
            CreateIfNotExists();
            var data = JsonSerializer.Deserialize<ConfigJSON>(File.ReadAllText(ConfigFile));


            if (data.TryGetValue(section, out var sectionData))
            {
                sectionData[key] = value;
            } else
            {
                data[section] = new()
                {
                    [key] = value
                };
            }
            File.WriteAllBytes(ConfigFile, JsonSerializer.SerializeToUtf8Bytes(data));
        }

        public string? Read(string key)
        {
            CreateIfNotExists();
            var data = JsonSerializer.Deserialize<ConfigJSON>(File.ReadAllText(ConfigFile));


            if (data.TryGetValue(SectionName, out var sectionData))
            {
                if (sectionData.TryGetValue(key, out var value))
                {
                    return value;
                }
            }
            return null;
        }
        public void Write(string key, string value)
        {
            var section = SectionName;
            CreateIfNotExists();
            var data = JsonSerializer.Deserialize<ConfigJSON>(File.ReadAllText(ConfigFile));


            if (data.TryGetValue(section, out var sectionData))
            {
                sectionData[key] = value;
            }
            else
            {
                data[section] = new()
                {
                    [key] = value
                };
            }
            File.WriteAllBytes(ConfigFile, JsonSerializer.SerializeToUtf8Bytes(data));
        }
    }
}
