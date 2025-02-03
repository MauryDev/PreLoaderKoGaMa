
using System.Text.Json.Serialization;

namespace PreLoaderKoGaMa.Installer.Shared.Helpers;

public class DependencyInfo
{
    [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonPropertyName("value")]
    public string Value { get; set; }
}
