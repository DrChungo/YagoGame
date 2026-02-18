using System.Text.Json.Serialization;

namespace RoguelikeYago.Src.Definitions;

public class ItemDef
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = "";

    [JsonPropertyName("name")]
    public string Name { get; set; } = "";

    [JsonPropertyName("tier")]
    public int Tier { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; } = "";

    [JsonPropertyName("stats")]
    public StatsDef Stats { get; set; } = new();

    [JsonPropertyName("unique")]
    public bool Unique { get; set; }
}
