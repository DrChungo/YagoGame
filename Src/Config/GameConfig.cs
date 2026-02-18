using System.Text.Json.Serialization;

namespace RoguelikeYago.Src.Config;


public class GameConfig
{
    [JsonPropertyName("rng")]
    public RngConfig Rng { get; set; } = new();

    [JsonPropertyName("enemyGeneration")]
    public EnemyGenerationConfig EnemyGeneration { get; set; } = new();


    [JsonPropertyName("drops")]
    public DropsConfig Drops { get; set; } = new();
}
