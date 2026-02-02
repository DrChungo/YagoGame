using System.Text.Json.Serialization;

namespace RoguelikeYago.Src.Definitions
{
    public class NpcDef
    {
        [JsonPropertyName("id")] public string Id { get; set; } = "";
        [JsonPropertyName("name")] public string Name { get; set; } = "";

        // Texto que ve el jugador
        [JsonPropertyName("dialogue")] public string Dialogue { get; set; } = "";

        // Buff/debuff fijo (simple)
        [JsonPropertyName("modifier")] public StatModifierDef Modifier { get; set; } = new();

        // Opcional: tipo de evento ("afterBoss", "shopChance", etc.)
        [JsonPropertyName("eventType")] public string EventType { get; set; } = "";
    }
}
