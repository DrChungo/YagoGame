using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace RoguelikeYago.Src.Definitions
{
    public class BossDef
    {
        [JsonPropertyName("id")] public string Id { get; set; } = "";
        [JsonPropertyName("name")] public string Name { get; set; } = "";

        // Orden/numero del boss en la run (1..4)
        [JsonPropertyName("bossNumber")] public int BossNumber { get; set; }

        [JsonPropertyName("stats")] public StatsDef Stats { get; set; } = new();

        // ✅ Boss skills embebidas (player no las aprende)
        [JsonPropertyName("skills")] public List<EmbeddedSkillDef> Skills { get; set; } = new();
    }
}
