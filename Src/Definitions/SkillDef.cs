using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace RoguelikeYago.Src.Definitions
{
    public class SkillDef
    {
        [JsonPropertyName("id")] public string Id { get; set; } = "";
        [JsonPropertyName("name")] public string Name { get; set; } = "";
        [JsonPropertyName("power")] public int Power { get; set; }

        // Opcional: para restringir por clase / rareza / etc.
        [JsonPropertyName("allowedClasses")] public List<string> AllowedClasses { get; set; } = new();

        // Opcional (si lo usas)
        [JsonPropertyName("tags")] public List<string> Tags { get; set; } = new();
    }
}
