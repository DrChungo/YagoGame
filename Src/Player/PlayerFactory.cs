// FASE 9 – Creación del jugador desde classes.json + save

using System.Linq;
using RoguelikeYago.Src.Definitions;
using RoguelikeYago.Src.Persistence;
using RoguelikeYago.Src.Services;

namespace RoguelikeYago.Src.Player;
    
    //PlayerFactory es la clase que se encarga de crear el jugador desde classes.json + save
public class PlayerFactory
{
    private readonly ContentService _content;
    //PlayerFactory es el constructor de la clase PlayerFactory
    public PlayerFactory(ContentService content)
    {
        _content = content;
    }

    //CreateFromClass es el método que se encarga de crear el jugador desde classes.json
    //Todavia no tenemos save
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
//CopyStats es el método que se encarga de copiar las stats del jugador
    private static StatsDef CopyStats(StatsDef s) =>
        new()
        {
            Hp = s.Hp,
            Speed = s.Speed,
            Damage = s.Damage,
            Armor = s.Armor,
        };
}
