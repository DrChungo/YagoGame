using System.Collections.Generic;
using System.Text.Json.Serialization;
using RoguelikeYago.Src.Definitions;

namespace RoguelikeYago.Src.Persistence;

public class SaveFile
{
    [JsonPropertyName("version")]
    public int Version { get; set; }

    [JsonPropertyName("seed")]
    public int Seed { get; set; }

    [JsonPropertyName("progress")]
    public SaveProgress Progress { get; set; } = new();

    [JsonPropertyName("player")]
    public SavePlayer Player { get; set; } = new();
}

public sealed class SaveProgress
{
    [JsonPropertyName("roomsCleared")]
    public int RoomsCleared { get; set; }

    [JsonPropertyName("bossesDefeated")]
    public int BossesDefeated { get; set; }
}

public sealed class SavePlayer
{
    [JsonPropertyName("classId")]
    public string ClassId { get; set; } = "";

    [JsonPropertyName("stats")]
    public StatsDef Stats { get; set; } = new();

    [JsonPropertyName("skills")]
    public List<string> Skills { get; set; } = new();

    [JsonPropertyName("inventoryItemIds")]
    public List<string> InventoryItemIds { get; set; } = new();

    [JsonPropertyName("uniqueItemIdsSeen")]
    public List<string> UniqueItemIdsSeen { get; set; } = new();
}
