using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace RoguelikeYago.Src.Definitions;

public sealed class ClassDef
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = "";

    [JsonPropertyName("name")]
    public string Name { get; set; } = "";

    [JsonPropertyName("description")]
    public string Description { get; set; } = "";

    [JsonPropertyName("baseStats")]
    public StatsDef BaseStats { get; set; } = new();

    [JsonPropertyName("startingSkills")]
    public List<string> StartingSkills { get; set; } = new();
}
