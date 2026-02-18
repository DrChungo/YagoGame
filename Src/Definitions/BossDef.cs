using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace RoguelikeYago.Src.Definitions;

public class BossDef
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = "";

    [JsonPropertyName("name")]
    public string Name { get; set; } = "";

    [JsonPropertyName("order")]
    public int Order { get; set; }

    [JsonPropertyName("stats")]
    public StatsDef Stats { get; set; } = new();

    [JsonPropertyName("attacks")]
    public List<AttackDef> Attacks { get; set; } = new();
}
