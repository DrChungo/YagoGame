using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace RoguelikeYago.Src.Definitions
{
    public class DropTableDef
    {
        [JsonPropertyName("id")] public string Id { get; set; } = "";

        // Contexto: "afterRoom", "afterBoss", etc.
        [JsonPropertyName("context")] public string Context { get; set; } = "";

        [JsonPropertyName("entries")] public List<DropEntryDef> Entries { get; set; } = new();
    }

    public class DropEntryDef
    {
        // id de item (referencia a items.json)
        [JsonPropertyName("itemId")] public string ItemId { get; set; } = "";

        // peso para WeightedPicker
        [JsonPropertyName("weight")] public int Weight { get; set; }

        // opcional: si quieres limitar duplicados por tier o por run
        [JsonPropertyName("unique")] public bool Unique { get; set; }
    }
}
