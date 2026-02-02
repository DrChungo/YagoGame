using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace RoguelikeYago.Src.Definitions
{
    public class ItemsFile
    {
        [JsonPropertyName("items")] public List<ItemDef> Items { get; set; } = new();
    }

    public class SkillsFile
    {
        [JsonPropertyName("skills")] public List<SkillDef> Skills { get; set; } = new();
    }

    public class EnemiesFile
    {
        [JsonPropertyName("enemies")] public List<EnemyDef> Enemies { get; set; } = new();
    }

    public class BossesFile
    {
        [JsonPropertyName("bosses")] public List<BossDef> Bosses { get; set; } = new();
    }

    public class NpcsFile
    {
        [JsonPropertyName("npcs")] public List<NpcDef> Npcs { get; set; } = new();
    }

    public class DropTablesFile
    {
        [JsonPropertyName("dropTables")] public List<DropTableDef> DropTables { get; set; } = new();
    }
}
