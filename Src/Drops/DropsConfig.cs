using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace RoguelikeYago.Src.Config;

// ===================================
// FASE 6 – CONFIG DE RECOMPENSAS (drops) 1:1
// ===================================
public class DropsConfig
{
    [JsonPropertyName("uniqueItemsOncePerRun")]
    public bool UniqueItemsOncePerRun { get; set; } = true;

    [JsonPropertyName("afterRoom")]
    public DropTable AfterRoom { get; set; } = new();

    [JsonPropertyName("afterBoss")]
    public DropTable AfterBoss { get; set; } = new();
}

public sealed class DropTable
{
    [JsonPropertyName("rewardTypeWeights")]
    public Dictionary<string, int> RewardTypeWeights { get; set; } = new();

    [JsonPropertyName("tierWeightsByType")]
    public TierWeightsByType TierWeightsByType { get; set; } = new();
}

public sealed class TierWeightsByType
{
    [JsonPropertyName("item")]
    public Dictionary<string, int> Item { get; set; } = new();

    [JsonPropertyName("skill")]
    public Dictionary<string, int> Skill { get; set; } = new();
}
