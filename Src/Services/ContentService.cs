using System.Collections.Generic;
using RoguelikeYago.Src.Config;
using RoguelikeYago.Src.Definitions;

namespace RoguelikeYago.Src.Services;

//Fase 0
public class ContentService
{
    private readonly JsonFileLoader _loader = new();

    public IReadOnlyList<ClassDef> Classes { get; }
    public IReadOnlyList<SkillDef> Skills { get; }
    public IReadOnlyList<EnemyDef> Enemies { get; }
    public IReadOnlyList<BossDef> Bosses { get; }
    public IReadOnlyList<ItemDef> Items { get; }
    public IReadOnlyList<NpcDef> Npcs { get; }

    public ContentService()
    {
        Classes = _loader.LoadList<ClassDef>(PathConfig.ClassesFile);
        Skills = _loader.LoadList<SkillDef>(PathConfig.SkillsFile);
        Enemies = _loader.LoadList<EnemyDef>(PathConfig.EnemiesFile);
        Bosses = _loader.LoadList<BossDef>(PathConfig.BossesFile);
        Items = _loader.LoadList<ItemDef>(PathConfig.ItemsFile);
        Npcs = _loader.LoadList<NpcDef>(PathConfig.NpcsFile);
    }
}
