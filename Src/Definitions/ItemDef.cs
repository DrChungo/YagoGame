using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace RoguelikeYago.Src.Definitions
{
    public class ItemDef
    {
        [JsonPropertyName("id")] public string Id { get; set; } = "";
        [JsonPropertyName("name")] public string Name { get; set; } = "";
        [JsonPropertyName("tier")] public int Tier { get; set; }

        // Bonos simples (si lo tenías así)
        [JsonPropertyName("modifiers")] public StatModifierDef Modifiers { get; set; } = new();

        // Opcional
        [JsonPropertyName("tags")] public List<string> Tags { get; set; } = new();
    }
}
