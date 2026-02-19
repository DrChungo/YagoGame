using RoguelikeYago.Src.Definitions;

namespace RoguelikeYago.Src.Combat;

public sealed class EnemyInstance
{
    public string Name { get; }
    public StatsDef Stats { get; }
    public AttackDef Attack { get; }

    public EnemyInstance(string name, StatsDef stats, AttackDef attack)
    {
        Name = name;
        Stats = stats;
        Attack = attack;
    }
}

internal record TurnActor(string Name, int Speed, bool IsPlayer, Action Act);
