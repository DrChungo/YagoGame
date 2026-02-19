using System;
using System.Collections.Generic;
using System.Linq;
using RoguelikeYago.Src.Combat;
using RoguelikeYago.Src.Definitions;

namespace RoguelikeYago.Src.Runs;

// ===================================
// FASE 7 – Helpers compartidos
// ===================================
public static class RunHelpers
{
    public static List<EnemyDef> GenerateRoom(
        IReadOnlyList<EnemyDef> enemies,
        Dictionary<string, int> tierWeights,
        Random rng
    )
    {
        var parsed = tierWeights.Select(kv => (tier: int.Parse(kv.Key), weight: kv.Value)).ToList();

        return Enumerable
            .Range(0, 3)
            .Select(_ =>
            {
                int roll = Pick(parsed, rng);
                return enemies.Where(e => e.Tier == roll).OrderBy(_ => rng.Next()).First();
            })
            .ToList();
    }

    public static List<EnemyInstance> ToInstances(List<EnemyDef> enemies)
    {
        return enemies
            .Select(e => new EnemyInstance(e.Name, Clone(e.Stats), e.Attack))
            .ToList();
    }

    private static int Pick(List<(int tier, int weight)> w, Random rng)
    {
        int total = w.Sum(x => x.weight);
        int r = rng.Next(1, total + 1);
        int acc = 0;
        foreach (var x in w)
        {
            acc += x.weight;
            if (r <= acc)
                return x.tier;
        }
        return w.Last().tier;
    }

    private static StatsDef Clone(StatsDef s) =>
        new StatsDef
        {
            Hp = s.Hp,
            Speed = s.Speed,
            Damage = s.Damage,
            Armor = s.Armor,
        };
}
