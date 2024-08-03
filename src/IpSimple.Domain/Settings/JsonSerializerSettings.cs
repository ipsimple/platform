using System.Text.Json;

namespace IpSimple.Domain.Settings;

public static class JsonSerializerSettings
{
    public static readonly JsonSerializerOptions DefaultJsonSerializer = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = false // Set to false for performance reasons
    };
}
