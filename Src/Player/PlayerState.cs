// FASE 9 – Estado runtime del jugador (clase, stats, skills)

using System.Collections.Generic;
using System.Linq;
using RoguelikeYago.Src.Definitions;

namespace RoguelikeYago.Src.Player;

//PlayerState es la clase que se encarga del estado del jugador
public class PlayerState
{
    public string ClassId { get; }
    public StatsDef Stats { get; private set; }
    public List<SkillDef> Skills { get; }

    public PlayerState(string classId, StatsDef stats, IEnumerable<SkillDef> skills)
    {
        ClassId = classId;
        Stats = stats;
        Skills = skills.Take(4).ToList();
    }

    //ReplaceSkill es el método que se encarga de reemplazar una skill del jugador
    //todavia no tenemos usamos skills
    public void ReplaceSkill(SkillDef newSkill, int index)
    {
        if (index < 0 || index >= Skills.Count)
            return;

        Skills[index] = newSkill;
    }

    //HasSkill es el método que se encarga de verificar si el jugador tiene una skill
    //todavia no tenemos usamos skills
    public bool HasSkill(string skillId) => Skills.Any(s => s.Id == skillId);
}
