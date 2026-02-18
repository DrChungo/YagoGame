using System.Text.Json;
using System.Text.Json.Serialization;

namespace RoguelikeYago.Src.Definitions;

public class NpcDef
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = "";

    [JsonPropertyName("name")]
    public string Name { get; set; } = "";

    [JsonPropertyName("type")]
    public string Type { get; set; } = "";

    [JsonPropertyName("chance")]
    public double? Chance { get; set; }

    [JsonPropertyName("effect")]
    public JsonElement Effect { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; } = "";
}
