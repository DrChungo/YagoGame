using System.Text.Json.Serialization;

namespace RoguelikeYago.Src.Definitions;

public sealed class StatsDef
{
    [JsonPropertyName("hp")]
    public int Hp { get; set; }

    [JsonPropertyName("speed")]
    public int Speed { get; set; }

    [JsonPropertyName("damage")]
    public int Damage { get; set; }

    [JsonPropertyName("armor")]
    public int Armor { get; set; }
}

