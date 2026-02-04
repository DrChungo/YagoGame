using System.Text.Json.Serialization;

namespace RoguelikeYago.Src.Definitions;

public sealed class EnemyDef
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = "";

    [JsonPropertyName("name")]
    public string Name { get; set; } = "";

    [JsonPropertyName("tier")]
    public int Tier { get; set; }

    [JsonPropertyName("stats")]
    public StatsDef Stats { get; set; } = new();

    [JsonPropertyName("attack")]
    public AttackDef Attack { get; set; } = new();
}
