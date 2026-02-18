// FASE 9 – Estado runtime del jugador (clase, stats, skills)

using System.Collections.Generic;
using System.Linq;
using RoguelikeYago.Src.Definitions;

namespace RoguelikeYago.Src.Player;

public class PlayerState
{
    public string ClassId { get; }
    public StatsDef Stats { get; private set; }
    public List<SkillDef> Skills { get; }

    public PlayerState(string classId, StatsDef stats, IEnumerable<SkillDef> skills)
    {
        ClassId = classId;
        Stats = stats;
        Skills = skills.Take(4).ToList(); // regla dura: máx 4 skills
    }

    public void ReplaceSkill(SkillDef newSkill, int index)
    {
        if (index < 0 || index >= Skills.Count)
            return;

        Skills[index] = newSkill;
    }

    public bool HasSkill(string skillId) => Skills.Any(s => s.Id == skillId);
}
