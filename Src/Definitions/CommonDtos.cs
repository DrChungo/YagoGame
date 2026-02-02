using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace RoguelikeYago.Src.Definitions
{
    public class StatsDef
    {
        [JsonPropertyName("hp")] public int Hp { get; set; }
        [JsonPropertyName("attack")] public int Attack { get; set; }
        [JsonPropertyName("defense")] public int Defense { get; set; }
        [JsonPropertyName("speed")] public int Speed { get; set; }
    }

    // Skill embebida para enemigos/bosses (no viene de skills.json)
    public class EmbeddedSkillDef
    {
        [JsonPropertyName("id")] public string Id { get; set; } = "";
        [JsonPropertyName("name")] public string Name { get; set; } = "";
        [JsonPropertyName("power")] public int Power { get; set; }

        // Opcional: por si metes tipo/elemento/tags
        [JsonPropertyName("tags")] public List<string> Tags { get; set; } = new();
    }

    // Por si NPCs/items aplican buffs simples (opcional)
    public class StatModifierDef
    {
        [JsonPropertyName("hp")] public int Hp { get; set; }
        [JsonPropertyName("attack")] public int Attack { get; set; }
        [JsonPropertyName("defense")] public int Defense { get; set; }
        [JsonPropertyName("speed")] public int Speed { get; set; }
    }
}
