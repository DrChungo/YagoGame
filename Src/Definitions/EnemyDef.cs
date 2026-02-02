using System.Text.Json.Serialization;

namespace RoguelikeYago.Src.Definitions
{
    public class EnemyDef
    {
        [JsonPropertyName("id")] public string Id { get; set; } = "";
        [JsonPropertyName("name")] public string Name { get; set; } = "";

        // Para filtrar por fase/dificultad (lo que ya dijiste)
        [JsonPropertyName("phase")] public int Phase { get; set; }

        [JsonPropertyName("stats")] public StatsDef Stats { get; set; } = new();

        // ✅ Opción A: el enemigo lleva su ataque fijo aquí
        [JsonPropertyName("skill")] public EmbeddedSkillDef Skill { get; set; } = new();
    }
}
