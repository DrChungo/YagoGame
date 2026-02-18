using System.Text.Json.Serialization;

namespace RoguelikeYago.Src.Definitions;

public class SkillDef
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = "";

    [JsonPropertyName("name")]
    public string Name { get; set; } = "";

    [JsonPropertyName("tier")]
    public int Tier { get; set; }

    [JsonPropertyName("damage")]
    public int Damage { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; } = "";
}
