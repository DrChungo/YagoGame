using System.Collections.Generic;
using System.Linq;
using RoguelikeYago.Src.Config;
using RoguelikeYago.Src.Definitions;

namespace RoguelikeYago.Src.Services;

// NUEVO
public sealed class ContentService
{
    private readonly JsonFileLoader _loader = new();

    public List<ClassDef> Classes =>
        _loader.LoadList<ClassDef>(PathConfig.ClassesFile);

    public List<SkillDef> Skills =>
        _loader.LoadList<SkillDef>(PathConfig.SkillsFile);

    public List<EnemyDef> Enemies =>
        _loader.LoadList<EnemyDef>(PathConfig.EnemiesFile);

    public List<BossDef> Bosses =>
        _loader.LoadList<BossDef>(PathConfig.BossesFile);

    public List<ItemDef> Items =>
        _loader.LoadList<ItemDef>(PathConfig.ItemsFile);

    public List<NpcDef> Npcs =>
        _loader.LoadList<NpcDef>(PathConfig.NpcsFile);

    // LINQ puro
    public ClassDef GetClass(string id) =>
        Classes.Single(c => c.Id == id);

    public SkillDef GetSkill(string id) =>
        Skills.Single(s => s.Id == id);
}
