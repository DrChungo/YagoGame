using System.Text.Json.Serialization;

namespace RoguelikeYago.Src.Config;

// ==========================
// FASE 4 – RNG CONFIG (MÍNIMO)
// ==========================
public class RngConfig
{
    [JsonPropertyName("defaultSeed")]
    public int DefaultSeed { get; set; } = 12345;
}
