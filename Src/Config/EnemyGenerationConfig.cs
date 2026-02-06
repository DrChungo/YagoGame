using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace RoguelikeYago.Src.Config;

// =======================================
// FASE 4 – ENEMY GENERATION (tierWeights)
// =======================================
public sealed class EnemyGenerationConfig
{
    [JsonPropertyName("tierWeights")]
    public Dictionary<string, int> TierWeights { get; set; } = new();
}