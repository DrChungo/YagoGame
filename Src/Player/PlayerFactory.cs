// FASE 9 – Creación del jugador desde classes.json + save

using System.Linq;
using RoguelikeYago.Src.Definitions;
using RoguelikeYago.Src.Persistence;
using RoguelikeYago.Src.Services;

namespace RoguelikeYago.Src.Player;

public class PlayerFactory
{
    private readonly ContentService _content;

    public PlayerFactory(ContentService content)
    {
        _content = content;
    }

    public PlayerState CreateFromClass(string classId)
    {
        var classDef = _content.Classes.First(c => c.Id == classId);

        var skills = _content
            .Skills.Where(s => classDef.StartingSkills.Contains(s.Id))
            .Take(4)
            .ToList();

        return new PlayerState(classDef.Id, CopyStats(classDef.BaseStats), skills);
    }

    public PlayerState CreateFromSave(SaveFile save)
    {
        // Validación mínima: que exista la clase en el contenido
        _content.Classes.First(c => c.Id == save.Player.ClassId);

        var skills = _content.Skills.Where(s => save.Player.Skills.Contains(s.Id)).Take(4).ToList();

        return new PlayerState(save.Player.ClassId, CopyStats(save.Player.Stats), skills);
    }

    private static StatsDef CopyStats(StatsDef s) =>
        new()
        {
            Hp = s.Hp,
            Speed = s.Speed,
            Damage = s.Damage,
            Armor = s.Armor,
        };
}
