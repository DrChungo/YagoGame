using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace RoguelikeYago.Src.Config;


public class EnemyGenerationConfig
{
    [JsonPropertyName("tierWeights")]
    public Dictionary<string, int> TierWeights { get; set; } = new();
}
