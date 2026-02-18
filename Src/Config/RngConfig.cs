using System.Text.Json.Serialization;

namespace RoguelikeYago.Src.Config;


// RNG CONFIG DE LA SEMILLA Default

public class RngConfig
{
    [JsonPropertyName("defaultSeed")]
    public int DefaultSeed { get; set; } = 12345;
}
