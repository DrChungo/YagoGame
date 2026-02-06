using System.Text.Json.Serialization;

namespace RoguelikeYago.Src.Config;

// ==========================
// FASE 4 – MODELOS CONFIG (MÍNIMO)
// ==========================
public sealed class GameConfig
{
    [JsonPropertyName("rng")]
    public RngConfig Rng { get; set; } = new();

    [JsonPropertyName("enemyGeneration")]
    public EnemyGenerationConfig EnemyGeneration { get; set; } = new();
//FASE 6
    [JsonPropertyName("drops")]
    public DropsConfig Drops { get; set; } = new();
}