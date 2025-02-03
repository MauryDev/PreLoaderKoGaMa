

using System.Text.Json.Serialization;

namespace PreLoaderKoGaMa.Installer.Shared.Helpers;

public class PackageInfo
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("dependencies")]
    public List<DependencyInfo> Dependencies { get; set; }
}
