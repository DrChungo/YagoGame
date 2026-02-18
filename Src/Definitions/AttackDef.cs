using System.Text.Json.Serialization;

namespace RoguelikeYago.Src.Definitions;

public class AttackDef
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = "";

    [JsonPropertyName("damage")]
    public int Damage { get; set; }
}
